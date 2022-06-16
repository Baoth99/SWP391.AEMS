using AEMS.Application;
using AEMS.Utilities;
using AEMS.WebApi.SystemConfigurations;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AEMS.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region Configuration Helper

            services.AddSingleton(Configuration);
            ConfigurationHelper.Configuration = Configuration;
            AppFileHelper.ContentRootPath = Environment.ContentRootPath;

            #endregion

            #region Api Version

            services.AddApiVersionSetUp();

            #endregion

            #region Proxy servers and load balancers 

            // Configure ASP.NET Core to work with proxy servers and load balancers
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            #endregion

            #region Controller 

            services.AddControllers();

            #endregion

            #region Database Connection

            services.AddDatabaseConnectionSetUp();

            #endregion

            #region Dependency Injection

            services.AddDependencyInjectionSetUp();

            #endregion

            #region AddMediatR

            services.AddMediatR(typeof(BaseDataAccess).Assembly);

            #endregion

            services.AddSignalR()
                    .AddAzureSignalR(opt =>
                    {
                        opt.ConnectionString = AppSettingValues.AzureSignalRConnection;
                    });

            services.AddApplicationInsightsTelemetry(x =>
            {
                x.ConnectionString = AppSettingValues.ApplicationInsightsConnectionString;
                x.InstrumentationKey = AppSettingValues.ApplicationInsightsInstrumentationKey;
            });

            #region Authentication

            services.AddAuthenticationSetUp();

            #endregion

            #region Use Swagger UI

            services.AddSwaggerGenSetUp();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(option => option
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod()
            );

            app.UseSwaggerGenSetUp();

            app.UseExceptionHandlerSetUp();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
