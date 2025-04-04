using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using WebAPICommonPitfalls.Features.SalesOrderDetail.Services;
using Carter;

namespace WebAPICommonPitfalls.Features.SalesOrderDetail.Endpoints
{
    public class SalesOrderDetailModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/salesorderdetails", async (ISalesOrderDetailService salesOrderDetailService) =>
            {
                var salesOrderDetails = await salesOrderDetailService.GetAllAsync();
                return Results.Ok(salesOrderDetails);
            });

            app.MapGet("/salesorderdetails/{id}", async (int id, ISalesOrderDetailService salesOrderDetailService) =>
            {
                var salesOrderDetail = await salesOrderDetailService.GetByIdAsync(id);
                return salesOrderDetail != null ? Results.Ok(salesOrderDetail) : Results.NotFound();
            });

            app.MapPost("/salesorderdetails", async (Models.SalesOrderDetail salesOrderDetail, ISalesOrderDetailService salesOrderDetailService) =>
            {
                var createdSalesOrderDetail = await salesOrderDetailService.CreateAsync(salesOrderDetail);
                return Results.Created($"/salesorderdetails/{createdSalesOrderDetail.SalesOrderID}", createdSalesOrderDetail);
            });

            app.MapPut("/salesorderdetails/{id}", async (int id, Models.SalesOrderDetail salesOrderDetail, ISalesOrderDetailService salesOrderDetailService) =>
            {
                var updatedSalesOrderDetail = await salesOrderDetailService.UpdateAsync(id, salesOrderDetail);
                return Results.Ok(updatedSalesOrderDetail);
            });

            app.MapDelete("/salesorderdetails/{id}", async (int id, ISalesOrderDetailService salesOrderDetailService) =>
            {
                var deleted = await salesOrderDetailService.DeleteAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
