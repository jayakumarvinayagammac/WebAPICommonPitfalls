using System;

namespace WebAPICommonPitfalls.Features.SalesOrderDetail.Models
{
    public record SalesOrderDetail(
        int SalesOrderID,
        int SalesOrderDetailID,
        string CarrierTrackingNumber,
        int OrderQty,
        int ProductID,
        int SpecialOfferID,
        decimal UnitPrice,
        decimal UnitPriceDiscount,
        decimal LineTotal,
        Guid RowGuid,
        DateTime ModifiedDate
    );
}