using WebAPICommonPitfalls.Common.Utilities;
using WebAPICommonPitfalls.Features.SalesOrderDetail.Repositories;
using Models = WebAPICommonPitfalls.Features.SalesOrderDetail.Models;
namespace WebAPICommonPitfalls.Features.SalesOrderDetail.Queries
{
    public record CollectionShipmentQuery(PaginationFilter Pagination, string endpoint): IQuery<Result<PagedCollection<Models.SalesOrderDetail>>>;
    public class CollectionShipmentQueryHandler : IQueryHandler<CollectionShipmentQuery, Result<PagedCollection<Models.SalesOrderDetail>>>
    {
        private readonly ISalesOrderDetailRepository _salesOrderDetailRepository;
        private readonly ILogger<CollectionShipmentQueryHandler> _logger;
        private readonly string _baseUrl;

        public CollectionShipmentQueryHandler(ISalesOrderDetailRepository salesOrderDetailRepository, ILogger<CollectionShipmentQueryHandler> logger, IConfiguration configuration)
        {
            _logger = logger;
            _logger.LogInformation("CollectionShipmentQueryHandler initialized.");
            _salesOrderDetailRepository = salesOrderDetailRepository;
            _baseUrl = configuration.GetValue<string>("BaseUrl") ?? throw new ArgumentNullException("BaseUrl is not configured in appsettings.json");
        }      
        public async Task<Result<PagedCollection<Models.SalesOrderDetail>>> Handle(CollectionShipmentQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CollectionShipmentQuery with Pagination: Page {Page}, PageSize {PageSize}", request.Pagination.PageNumber, request.Pagination.PageSize);
            var page = request.Pagination.PageNumber; // Default page number
            var pageSize = request.Pagination.PageSize; // Default page size
            var salesOrderDetails = await _salesOrderDetailRepository.GetAllAsync(); // Assuming this returns IEnumerable
            // Use synchronous LINQ methods for in-memory collections
            var totalCount = salesOrderDetails.Count();
            var pagedData = salesOrderDetails.Skip((page - 1) * pageSize).Take(pageSize);
            var baseUri = $"{_baseUrl}/{request.endpoint}"; // Assuming this is the base URI for your API
            var pagedCollection = new PagedCollection<Models.SalesOrderDetail>( pagedData, page, pageSize, totalCount, baseUri);
            return Result<PagedCollection<Models.SalesOrderDetail>>.Success(pagedCollection);
        }

    }
    
}