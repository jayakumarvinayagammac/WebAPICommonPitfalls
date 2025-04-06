using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Carter;
using MediatR;
using WebAPICommonPitfalls.Features.SalesOrderDetail.Queries;
using Microsoft.AspNetCore.Mvc;
using WebAPICommonPitfalls.Common.Utilities;
using FluentValidation;

namespace WebAPICommonPitfalls.Features.SalesOrderDetail.Endpoints
{
    public class SalesOrderDetailModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {            
            app.MapGet("/salesorderdetails", async (ISender sender, [AsParameters] PaginationFilter paginationFilter, 
            ILogger<SalesOrderDetailModule> logger, HttpContext httpContext, IValidator<PaginationFilter> validator) =>
            {
                // Validate the pagination filter using FluentValidation
                var validationResult = await validator.ValidateAsync(paginationFilter);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
                }
                // // Validate pagination filter
                // if (paginationFilter.PageNumber <= 0 || paginationFilter.PageSize <= 0)
                // {
                //     return Results.BadRequest("Invalid pagination parameters.");
                // }
                // Log the request
                logger.LogInformation("Fetching sales order details with pagination: {PageNumber}, {PageSize}", paginationFilter.PageNumber, paginationFilter.PageSize);
                // Extract routing details from the request
                var routePattern = (httpContext.GetEndpoint()?.DisplayName!.Split('/').Last() ?? string.Empty).ToLowerInvariant();
                // Use the sender to send the query and get the result
                var query = new CollectionShipmentQuery(paginationFilter, routePattern);
                var result = await sender.Send(query);
                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
            });
        }
    }
}
