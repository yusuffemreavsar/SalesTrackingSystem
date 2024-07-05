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

namespace Application.Features.SalesDetails.Commands.Create;

public class CreateSalesDetailCommand : IRequest<CreatedSalesDetailResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid SaleId { get; set; }
    public Sale Sale { get; set; }
    public Guid ProductSale { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }

    public string[] Roles => [Admin, Write, SalesDetailsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSalesDetails"];

    public class CreateSalesDetailCommandHandler : IRequestHandler<CreateSalesDetailCommand, CreatedSalesDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISalesDetailRepository _salesDetailRepository;
        private readonly SalesDetailBusinessRules _salesDetailBusinessRules;

        public CreateSalesDetailCommandHandler(IMapper mapper, ISalesDetailRepository salesDetailRepository,
                                         SalesDetailBusinessRules salesDetailBusinessRules)
        {
            _mapper = mapper;
            _salesDetailRepository = salesDetailRepository;
            _salesDetailBusinessRules = salesDetailBusinessRules;
        }

        public async Task<CreatedSalesDetailResponse> Handle(CreateSalesDetailCommand request, CancellationToken cancellationToken)
        {
            SalesDetail salesDetail = _mapper.Map<SalesDetail>(request);

            await _salesDetailRepository.AddAsync(salesDetail);

            CreatedSalesDetailResponse response = _mapper.Map<CreatedSalesDetailResponse>(salesDetail);
            return response;
        }
    }
}