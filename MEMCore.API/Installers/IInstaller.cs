using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MEMCore.API.Installers
{
    interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration Config);
    }
}
