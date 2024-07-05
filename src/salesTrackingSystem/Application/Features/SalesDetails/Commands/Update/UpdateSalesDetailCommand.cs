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

namespace Application.Features.SalesDetails.Commands.Update;

public class UpdateSalesDetailCommand : IRequest<UpdatedSalesDetailResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; }
    public Guid ProductSale { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }

    public string[] Roles => [Admin, Write, SalesDetailsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSalesDetails"];

    public class UpdateSalesDetailCommandHandler : IRequestHandler<UpdateSalesDetailCommand, UpdatedSalesDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISalesDetailRepository _salesDetailRepository;
        private readonly SalesDetailBusinessRules _salesDetailBusinessRules;

        public UpdateSalesDetailCommandHandler(IMapper mapper, ISalesDetailRepository salesDetailRepository,
                                         SalesDetailBusinessRules salesDetailBusinessRules)
        {
            _mapper = mapper;
            _salesDetailRepository = salesDetailRepository;
            _salesDetailBusinessRules = salesDetailBusinessRules;
        }

        public async Task<UpdatedSalesDetailResponse> Handle(UpdateSalesDetailCommand request, CancellationToken cancellationToken)
        {
            SalesDetail? salesDetail = await _salesDetailRepository.GetAsync(predicate: sd => sd.Id == request.Id, cancellationToken: cancellationToken);
            await _salesDetailBusinessRules.SalesDetailShouldExistWhenSelected(salesDetail);
            salesDetail = _mapper.Map(request, salesDetail);

            await _salesDetailRepository.UpdateAsync(salesDetail!);

            UpdatedSalesDetailResponse response = _mapper.Map<UpdatedSalesDetailResponse>(salesDetail);
            return response;
        }
    }
}