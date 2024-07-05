using NArchitecture.Core.Application.Responses;

namespace Application.Features.OrderDetails.Queries.GetById;

public class GetByIdOrderDetailResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}