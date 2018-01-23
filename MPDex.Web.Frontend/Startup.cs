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
using MPDex.Data;
using MPDex.Repository;
using MPDex.Services;
using Newtonsoft.Json.Serialization;

namespace MPDex.Web.Frontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // inject dbcontext
            services.AddDbContext<MPDexContext>(options => options
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                    b => b.MigrationsAssembly("MPDex.Data")));

            // inject identity
            services.AddIdentity<Operator, OperatorRole>()
                .AddEntityFrameworkStores<MPDexContext>()
                .AddDefaultTokenProviders();

            // inject unit of work
            services.AddUnitOfWork<MPDexContext>();

            // inject coin service
            services.AddScoped<ICoinService, CoinService>();

            // inject customer service
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IFeeService, FeeService>();

            // inject email service
            services.AddTransient<IEmailSender, EmailSender>();

            // inject mvc with json camel case resolver
            services.AddMvc()
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            // inject auto mapper
            services.AddAutoMapper();

            // client ip address information
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public static readonly LoggerFactory MyLoggerFactory
            = new LoggerFactory(new[] 
            { new ConsoleLoggerProvider((category, level) => 
                category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information, true)
            });

    }
}
