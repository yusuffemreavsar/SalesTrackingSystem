using Application.Features.Sales.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Sales.Constants.SalesOperationClaims;

namespace Application.Features.Sales.Queries.GetList;

public class GetListSaleQuery : IRequest<GetListResponse<GetListSaleListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListSales({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetSales";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListSaleQueryHandler : IRequestHandler<GetListSaleQuery, GetListResponse<GetListSaleListItemDto>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetListSaleQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSaleListItemDto>> Handle(GetListSaleQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Sale> sales = await _saleRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSaleListItemDto> response = _mapper.Map<GetListResponse<GetListSaleListItemDto>>(sales);
            return response;
        }
    }
}