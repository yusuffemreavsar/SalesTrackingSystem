using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.SalesDetails.Queries.GetList;

public class GetListSalesDetailListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; }
    public Guid ProductSale { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}