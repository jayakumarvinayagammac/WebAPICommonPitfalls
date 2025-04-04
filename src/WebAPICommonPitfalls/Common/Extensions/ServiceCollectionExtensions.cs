using Microsoft.Extensions.DependencyInjection;
using WebAPICommonPitfalls.Features.SalesOrderDetail.Services;
using WebAPICommonPitfalls.Features.SalesOrderDetail.Repositories; // Add this

namespace WebAPICommonPitfalls.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            // Register service and repository
            services.AddScoped<ISalesOrderDetailService, SalesOrderDetailService>();
            services.AddScoped<ISalesOrderDetailRepository, SalesOrderDetailRepository>();
            return services;
        }
    }
}