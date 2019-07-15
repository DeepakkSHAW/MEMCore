using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

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



            //*API Documentation Swagger dependency Injection started*//
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info() { Title = "MEM Api", Version = "v1" });
            });
            //*API Documentation Ends*//

            services.AddScoped<MEMCore.Services.IExpenseRepository, MEMCore.Services.ExpenseRepository>();
            services.AddScoped<MEMCore.Services.ICategoryRepository, MEMCore.Services.CategoryRepository>();
            services.AddScoped<MEMCore.Services.ICurrencyRepository, MEMCore.Services.CurrencyRepository>();
            //*Introduced Auto mapper *//
            services.AddAutoMapper();
             
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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api for Monthly Expense Management");
            });
            //*API Documentation Ends*//

            app.UseMvc();
        }
    }
}
