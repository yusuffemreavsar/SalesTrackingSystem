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

namespace Application.Features.Sales.Commands.Update;

public class UpdateSaleCommand : IRequest<UpdatedSaleResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public int TotalPrice { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }

    public string[] Roles => [Admin, Write, SalesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetSales"];

    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, UpdatedSaleResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;
        private readonly SaleBusinessRules _saleBusinessRules;

        public UpdateSaleCommandHandler(IMapper mapper, ISaleRepository saleRepository,
                                         SaleBusinessRules saleBusinessRules)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
            _saleBusinessRules = saleBusinessRules;
        }

        public async Task<UpdatedSaleResponse> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            Sale? sale = await _saleRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _saleBusinessRules.SaleShouldExistWhenSelected(sale);
            sale = _mapper.Map(request, sale);

            await _saleRepository.UpdateAsync(sale!);

            UpdatedSaleResponse response = _mapper.Map<UpdatedSaleResponse>(sale);
            return response;
        }
    }
}