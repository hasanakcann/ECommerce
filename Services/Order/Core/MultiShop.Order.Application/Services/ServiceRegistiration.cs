using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MultiShop.Order.Application.Services
{
    /// <summary>
    ///     Program.cs'den ServiceRegistiration sınıfına ait metotlara erişebilmek için static tanımlandı.
    /// </summary>
    public static class ServiceRegistiration
    {
        public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ServiceRegistiration).Assembly));
        }
    }
}
