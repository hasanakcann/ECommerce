using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MultiShop.Order.Application.Services
{
    /// <summary>
    ///     Program.cs'den ServiceRegistiration sınıfına ait metotlara erişebilmek için static tanımlandı.
    ///     Bu metot sayesinde program.cs'de mediatR'ı çağırmak zorunda kalmayacağım.
    /// </summary>
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));
        }
    }
}
