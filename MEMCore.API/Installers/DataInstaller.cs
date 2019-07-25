using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MEMCore.API.Installers
{
    public class DataInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Config)
        {
            services.AddScoped<MEMCore.Services.IExpenseRepository, MEMCore.Services.ExpenseRepository>();
            services.AddScoped<MEMCore.Services.ICategoryRepository, MEMCore.Services.CategoryRepository>();
            services.AddScoped<MEMCore.Services.ICurrencyRepository, MEMCore.Services.CurrencyRepository>();

            //*Introduced Auto mapper *//
            services.AddAutoMapper();
        }
    }
}
