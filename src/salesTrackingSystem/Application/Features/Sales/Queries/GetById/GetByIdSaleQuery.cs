using Application.Features.Sales.Constants;
using Application.Features.Sales.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Sales.Constants.SalesOperationClaims;

namespace Application.Features.Sales.Queries.GetById;

public class GetByIdSaleQuery : IRequest<GetByIdSaleResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdSaleQueryHandler : IRequestHandler<GetByIdSaleQuery, GetByIdSaleResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;
        private readonly SaleBusinessRules _saleBusinessRules;

        public GetByIdSaleQueryHandler(IMapper mapper, ISaleRepository saleRepository, SaleBusinessRules saleBusinessRules)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
            _saleBusinessRules = saleBusinessRules;
        }

        public async Task<GetByIdSaleResponse> Handle(GetByIdSaleQuery request, CancellationToken cancellationToken)
        {
            Sale? sale = await _saleRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _saleBusinessRules.SaleShouldExistWhenSelected(sale);

            GetByIdSaleResponse response = _mapper.Map<GetByIdSaleResponse>(sale);
            return response;
        }
    }
}