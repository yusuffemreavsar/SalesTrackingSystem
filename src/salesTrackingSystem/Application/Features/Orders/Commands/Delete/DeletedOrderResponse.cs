using NArchitecture.Core.Application.Responses;

namespace Application.Features.Orders.Commands.Delete;

public class DeletedOrderResponse : IResponse
{
    public Guid Id { get; set; }
}