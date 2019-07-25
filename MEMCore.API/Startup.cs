using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MEMCore.API.Installers;

namespace MEMCore.API
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
           //Installers.InstallerExtension.InstallServicesInAssembly(services, Configuration);
            services.InstallServicesInAssembly(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //*API Documentation Swagger implementation starts*//
            //Option1:
            //app.UseSwagger();
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Monthly Expense Management System Api's");
            //});

            //Option2: with custom configuration
            var swaggerConfig = new CConfiguration.SwaggerConfig();
            //Configuration.GetSection(nameof(CConfiguration.SwaggerConfig)).Bind(swaggerConfig);
            Configuration.GetSection("SwaggerSettings").Bind(swaggerConfig);
            app.UseSwagger(option =>
            {
                option.RouteTemplate = swaggerConfig.JsonRoute;
            });
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerConfig.UIEndpoint, swaggerConfig.Desrciption);
            });
            //*API Documentation Ends*//
            app.UseMvc();
        }
    }
}
