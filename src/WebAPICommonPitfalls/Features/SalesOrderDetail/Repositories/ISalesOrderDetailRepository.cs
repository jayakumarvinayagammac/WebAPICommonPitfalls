using Models = WebAPICommonPitfalls.Features.SalesOrderDetail.Models;
using System.Text.Json;
using System.IO; // Ensure this is also included for file operations
namespace WebAPICommonPitfalls.Features.SalesOrderDetail.Repositories
{
    public interface ISalesOrderDetailRepository
    {
        Task<IEnumerable<Models.SalesOrderDetail>> GetAllAsync();
        Task<Models.SalesOrderDetail?> GetByIdAsync(int id);
        Task<Models.SalesOrderDetail?> AddAsync(Models.SalesOrderDetail salesOrderDetail);
        Task<Models.SalesOrderDetail?> UpdateAsync(int id, Models.SalesOrderDetail salesOrderDetail);
        Task<int> DeleteAsync(int id);
    }

    public class SalesOrderDetailRepository : ISalesOrderDetailRepository
    {
        private readonly List<Models.SalesOrderDetail> _salesOrderDetails;

        public SalesOrderDetailRepository(string jsonFilePath = "g:\\Json Source\\SalesOrderDetail.json")
        {
            _salesOrderDetails = LoadSalesOrderDetailsFromJson(jsonFilePath);
        }

        private List<Models.SalesOrderDetail> LoadSalesOrderDetailsFromJson(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
            {
                return new List<Models.SalesOrderDetail>();
            }

            var jsonData = File.ReadAllText(jsonFilePath);
            return JsonSerializer.Deserialize<List<Models.SalesOrderDetail>>(jsonData) ?? new List<Models.SalesOrderDetail>();
        }

        public async Task<IEnumerable<Models.SalesOrderDetail>> GetAllAsync()
        {
            return await Task.Run(() => _salesOrderDetails.AsEnumerable());
        }

        public Task<Models.SalesOrderDetail?> GetByIdAsync(int id)
        {
            var salesOrderDetail = _salesOrderDetails.FirstOrDefault(so => so.SalesOrderDetailID == id);
            return Task.FromResult(salesOrderDetail);
        }

        public Task<Models.SalesOrderDetail?> AddAsync(Models.SalesOrderDetail salesOrderDetail)
        {
            _salesOrderDetails.Add(salesOrderDetail);
            return salesOrderDetail != null ? Task.FromResult(salesOrderDetail) : Task.FromResult<Models.SalesOrderDetail?>(null);
        }

        public Task<Models.SalesOrderDetail?> UpdateAsync(int id, Models.SalesOrderDetail salesOrderDetail)
        {
            var existingSalesOrderDetail = _salesOrderDetails.FirstOrDefault(so => so.SalesOrderDetailID == salesOrderDetail.SalesOrderDetailID);
            if (existingSalesOrderDetail != null)
            {
                _salesOrderDetails.Remove(existingSalesOrderDetail);
                _salesOrderDetails.Add(salesOrderDetail);
            }

            return Task.FromResult(existingSalesOrderDetail);
        }

        public Task<int> DeleteAsync(int id)
        {
            var salesOrderDetail = _salesOrderDetails.FirstOrDefault(so => so.SalesOrderDetailID == id);
            if (salesOrderDetail != null)
            {
                _salesOrderDetails.Remove(salesOrderDetail);
            }
            return Task.FromResult(salesOrderDetail != null ? id : 0);
        }
    }
}