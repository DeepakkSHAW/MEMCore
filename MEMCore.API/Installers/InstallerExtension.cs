using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MEMCore.API.Installers
{
    public static class InstallerExtension
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var Installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
                typeof(Installers.IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<Installers.IInstaller>().ToList();

            Installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
