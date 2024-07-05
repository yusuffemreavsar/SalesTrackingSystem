using NArchitecture.Core.Application.Dtos;

namespace Application.Features.OrderDetails.Queries.GetList;

public class GetListOrderDetailListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}