using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace MEMCore.API.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Config)
        {
            //*API Documentation Swagger dependency Injection started*//
            services.AddSwaggerGen(c =>
            {
                //shown at swagger logo
                c.SwaggerDoc("v1", new Info() { Title = "MEM API", Version = "v1.x" });
            });
            //*API Documentation Ends*//


            //*Introduced API Versioning*//
            services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ReportApiVersions = true;
                //* Version in URL itself *//
                //This approach taken because of swagger api documentation is easy to handle  
                opt.ApiVersionReader = new UrlSegmentApiVersionReader();
                //* Version embedded in message header or query parameters *//
                //opt.ApiVersionReader = ApiVersionReader.Combine
                //   (
                //       new HeaderApiVersionReader("X-Version"),
                //       new QueryStringApiVersionReader("ver", "version"));
            });
            services.AddMvc(opt => opt.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
    }
}
