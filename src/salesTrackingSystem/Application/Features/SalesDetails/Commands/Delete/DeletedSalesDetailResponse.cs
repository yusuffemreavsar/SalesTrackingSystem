using NArchitecture.Core.Application.Responses;

namespace Application.Features.SalesDetails.Commands.Delete;

public class DeletedSalesDetailResponse : IResponse
{
    public Guid Id { get; set; }
}