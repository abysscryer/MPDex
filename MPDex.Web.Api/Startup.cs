using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MPDex.CacheRepository;
using MPDex.Data;
using MPDex.Repository;
using MPDex.Services;
using MPDex.Web.Api.Hubs;
using MPDex.Web.Api.Middlewares;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace MPDex.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            services.AddDbContext<MPDexContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("MPDex.Data")));

            services.AddUnitOfWork<MPDexContext>();

            services.AddAutoMapper();

            services.Configure<RedisConfiguration>(Configuration.GetSection("redis"));

            services.AddSingleton<IRedisConnectionFactory, RedisConnectionFactory>();

            services.AddScoped<IBookCache, BookCache>();

            services.AddScoped<IBookService, BookService>();

            services.AddScoped<ICoinService, CoinService>();

            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IFeeService, FeeService>();

            services.AddSignalR();

            services
                .AddMvcCore()
                .AddJsonFormatters(options => 
                    options.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddAuthorization();

            services.AddCors();

            services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "http://localhost:5000";
                options.RequireHttpsMetadata = false;
                options.ApiName = "api1";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(policy =>
            {
                policy.WithOrigins("http://localhost:5002");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.WithExposedHeaders("WWW-Authenticate");
            });

            app.UseAuthentication();
            
            app.UseRemoteIpAddressLogging();

            app.UseSignalR(routes =>
            {
                routes.MapHub<BookHub>("book");
            });

            app.UseMvc();
        }
    }
}
