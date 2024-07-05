using NArchitecture.Core.Application.Responses;

namespace Application.Features.Orders.Queries.GetById;

public class GetByIdOrderResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}