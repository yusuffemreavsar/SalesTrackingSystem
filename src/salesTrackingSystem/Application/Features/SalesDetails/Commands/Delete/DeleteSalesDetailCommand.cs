using Application.Features.SalesDetails.Constants;
using Application.Features.SalesDetails.Constants;
using Application.Features.SalesDetails.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.SalesDetails.Constants.SalesDetailsOperationClaims;

namespace Application.Features.SalesDetails.Commands.Delete;

public class DeleteSalesDetailCommand : IRequest<DeletedSalesDetailResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, SalesDetailsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSalesDetails"];

    public class DeleteSalesDetailCommandHandler : IRequestHandler<DeleteSalesDetailCommand, DeletedSalesDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISalesDetailRepository _salesDetailRepository;
        private readonly SalesDetailBusinessRules _salesDetailBusinessRules;

        public DeleteSalesDetailCommandHandler(IMapper mapper, ISalesDetailRepository salesDetailRepository,
                                         SalesDetailBusinessRules salesDetailBusinessRules)
        {
            _mapper = mapper;
            _salesDetailRepository = salesDetailRepository;
            _salesDetailBusinessRules = salesDetailBusinessRules;
        }

        public async Task<DeletedSalesDetailResponse> Handle(DeleteSalesDetailCommand request, CancellationToken cancellationToken)
        {
            SalesDetail? salesDetail = await _salesDetailRepository.GetAsync(predicate: sd => sd.Id == request.Id, cancellationToken: cancellationToken);
            await _salesDetailBusinessRules.SalesDetailShouldExistWhenSelected(salesDetail);

            await _salesDetailRepository.DeleteAsync(salesDetail!);

            DeletedSalesDetailResponse response = _mapper.Map<DeletedSalesDetailResponse>(salesDetail);
            return response;
        }
    }
}