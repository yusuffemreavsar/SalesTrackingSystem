using Application.Features.Sales.Constants;
using Application.Features.Sales.Constants;
using Application.Features.Sales.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Sales.Constants.SalesOperationClaims;

namespace Application.Features.Sales.Commands.Delete;

public class DeleteSaleCommand : IRequest<DeletedSaleResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, SalesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSales"];

    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, DeletedSaleResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;
        private readonly SaleBusinessRules _saleBusinessRules;

        public DeleteSaleCommandHandler(IMapper mapper, ISaleRepository saleRepository,
                                         SaleBusinessRules saleBusinessRules)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
            _saleBusinessRules = saleBusinessRules;
        }

        public async Task<DeletedSaleResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            Sale? sale = await _saleRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _saleBusinessRules.SaleShouldExistWhenSelected(sale);

            await _saleRepository.DeleteAsync(sale!);

            DeletedSaleResponse response = _mapper.Map<DeletedSaleResponse>(sale);
            return response;
        }
    }
}