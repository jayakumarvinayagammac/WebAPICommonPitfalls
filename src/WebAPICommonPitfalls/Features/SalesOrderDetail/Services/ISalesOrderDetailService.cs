using Microsoft.Extensions.DependencyInjection;
using Repositories = WebAPICommonPitfalls.Features.SalesOrderDetail.Repositories;
using Models = WebAPICommonPitfalls.Features.SalesOrderDetail.Models; // Alias for Models namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPICommonPitfalls.Features.SalesOrderDetail.Services
{
    public interface ISalesOrderDetailService
    {
        Task<IEnumerable<Models.SalesOrderDetail>> GetAllAsync();
        Task<Models.SalesOrderDetail?> GetByIdAsync(int id);
        Task<Models.SalesOrderDetail> CreateAsync(Models.SalesOrderDetail salesOrderDetail);
        Task<Models.SalesOrderDetail?> UpdateAsync(int id, Models.SalesOrderDetail salesOrderDetail);
        Task<bool> DeleteAsync(int id);
    }

    public class SalesOrderDetailService : ISalesOrderDetailService
    {
        private readonly Repositories.ISalesOrderDetailRepository _repository;

        public SalesOrderDetailService(Repositories.ISalesOrderDetailRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Models.SalesOrderDetail>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Models.SalesOrderDetail?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Models.SalesOrderDetail> CreateAsync(Models.SalesOrderDetail salesOrderDetail)
        {
            return await _repository.AddAsync(salesOrderDetail);
        }

        public async Task<Models.SalesOrderDetail?> UpdateAsync(int id, Models.SalesOrderDetail salesOrderDetail)
        {
            return await _repository.UpdateAsync(id, salesOrderDetail);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id)>0;
        }
    }
}