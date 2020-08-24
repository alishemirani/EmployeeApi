using EmployeeApi.Repository;
using EmployeeApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeApi.Extensions
{
    public static class ServiceRegistryExtension {
        public static void RegisterServices(this IServiceCollection services) {
            services.Scan(t => t.FromCallingAssembly()
            .AddClasses(c => c.AssignableTo<IService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services.Scan(t => t.FromCallingAssembly()
            .AddClasses(c => c.AssignableTo<IRepository>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime());
        }
    }
}