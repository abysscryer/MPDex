using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using MPDex.CacheRepository;
using MPDex.Data;
using MPDex.Repository;
using MPDex.Services;
using MPDex.Web.Frontend.Hubs;
using MPDex.Web.Frontend.Middlewares;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;

namespace MPDex.Web.Frontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                //.MinimumLevel.Verbose()
                //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));
            
            // inject dbcontext
            services.AddDbContext<MPDexContext>(options => options
                //.UseLoggerFactory(MyLoggerFactory)
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                    b => b.MigrationsAssembly("MPDex.Data")));

            // inject identity
            services.AddIdentity<Operator, OperatorRole>()
                .AddEntityFrameworkStores<MPDexContext>()
                .AddDefaultTokenProviders();

            // inject unit of work
            services.AddUnitOfWork<MPDexContext>();

            // inject auto mapper
            services.AddAutoMapper();

            // config redis
            services.Configure<RedisConfiguration>(Configuration.GetSection("redis"));

            // inject redis connection
            services.AddSingleton<IRedisConnectionFactory, RedisConnectionFactory>();

            // client ip address information
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // inject bookCache
            services.AddScoped<IBookCache, BookCache>();

            // inject coin service
            services.AddScoped<ICoinService, CoinService>();

            // inject customer service
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IFeeService, FeeService>();
            
            // inject email service
            //services.AddTransient<IEmailSender, EmailSender>();

            // services.AddCors(action => action.AddPolicy("AllowAny", builder =>
            // {
            //     builder
            //         .AllowAnyHeader()
            //         .AllowAnyMethod()
            //         .AllowAnyOrigin();
            // }));

            // inject signalr
            services.AddSignalR();

            // inject mvc with json camel case resolver
            services.AddMvc()
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            //services.AddMvcCore()
            //    .AddAuthorization()
            //    .AddJsonFormatters();

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
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseRemoteIpAddressLogging();
            
            app.UseCors("AllowAny");

            app.UseSignalR(routes =>
            {
                routes.MapHub<BookHub>("book");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                if (!serviceScope.ServiceProvider.GetService<MPDexContext>().AllMigrationsApplied())
                {
                    serviceScope.ServiceProvider.GetService<MPDexContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<MPDexContext>().EnsureSeeded();
                }
            }
        }

        public static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[] 
            { new ConsoleLoggerProvider((category, level) => 
                category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information, true)
            });

    }
}
