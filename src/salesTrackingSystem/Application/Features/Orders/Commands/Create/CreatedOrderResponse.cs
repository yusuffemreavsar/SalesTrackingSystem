using NArchitecture.Core.Application.Responses;

namespace Application.Features.Orders.Commands.Create;

public class CreatedOrderResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}