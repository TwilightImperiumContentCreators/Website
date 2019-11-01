using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TwilightImperiumContentCreatorsCommunity.Controllers;
using TwilightImperiumContentCreatorsCommunity.Repositories;
using TwilightImperiumContentCreatorsCommunity.Repositories.AzureStorage;
using TwilightImperiumContentCreatorsCommunity.Services;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
namespace TwilightImperiumContentCreatorsCommunity
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
            services.AddRazorPages();
            services.AddApplicationInsightsTelemetry();
            services.AddApiVersioning();
            services.AddVersionedApiExplorer();
            services.AddOpenApiDocument();
            services.AddSingleton(CloudStorageAccount.Parse(Configuration.GetConnectionString("TimodStorage")).CreateCloudTableClient());
            services.AddSingleton<MapsRepository, AzureTableStorageMapsRepository>();
            services.AddSingleton<MapsService>();
            services.AddSingleton<MapsController>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthorization();
            app.UseApiVersioning();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
