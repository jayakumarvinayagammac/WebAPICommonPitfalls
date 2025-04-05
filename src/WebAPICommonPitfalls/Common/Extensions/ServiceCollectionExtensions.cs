using Microsoft.Extensions.DependencyInjection;
using WebAPICommonPitfalls.Features.SalesOrderDetail.Repositories;
using MediatR;
using WebAPICommonPitfalls.Features.SalesOrderDetail.Queries; // Add this
using Models = WebAPICommonPitfalls.Features.SalesOrderDetail.Models;
using WebAPICommonPitfalls.Common.Utilities;

namespace WebAPICommonPitfalls.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            // Register service and repository
            services.AddScoped<IRequestHandler<CollectionShipmentQuery, Result<PagedCollection<Models.SalesOrderDetail>>>, CollectionShipmentQueryHandler>();
            services.AddScoped<ISalesOrderDetailRepository, SalesOrderDetailRepository>();
            return services;
        }
    }
}