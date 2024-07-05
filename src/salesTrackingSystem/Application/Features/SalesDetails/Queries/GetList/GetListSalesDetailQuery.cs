using Application.Features.SalesDetails.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.SalesDetails.Constants.SalesDetailsOperationClaims;

namespace Application.Features.SalesDetails.Queries.GetList;

public class GetListSalesDetailQuery : IRequest<GetListResponse<GetListSalesDetailListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListSalesDetails({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetSalesDetails";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListSalesDetailQueryHandler : IRequestHandler<GetListSalesDetailQuery, GetListResponse<GetListSalesDetailListItemDto>>
    {
        private readonly ISalesDetailRepository _salesDetailRepository;
        private readonly IMapper _mapper;

        public GetListSalesDetailQueryHandler(ISalesDetailRepository salesDetailRepository, IMapper mapper)
        {
            _salesDetailRepository = salesDetailRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListSalesDetailListItemDto>> Handle(GetListSalesDetailQuery request, CancellationToken cancellationToken)
        {
            IPaginate<SalesDetail> salesDetails = await _salesDetailRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListSalesDetailListItemDto> response = _mapper.Map<GetListResponse<GetListSalesDetailListItemDto>>(salesDetails);
            return response;
        }
    }
}