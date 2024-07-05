using NArchitecture.Core.Application.Responses;

namespace Application.Features.SalesDetails.Commands.Update;

public class UpdatedSalesDetailResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; }
    public Guid ProductSale { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}