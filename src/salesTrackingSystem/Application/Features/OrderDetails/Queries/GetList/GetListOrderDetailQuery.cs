using Application.Features.OrderDetails.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.OrderDetails.Constants.OrderDetailsOperationClaims;

namespace Application.Features.OrderDetails.Queries.GetList;

public class GetListOrderDetailQuery : IRequest<GetListResponse<GetListOrderDetailListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListOrderDetails({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetOrderDetails";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListOrderDetailQueryHandler : IRequestHandler<GetListOrderDetailQuery, GetListResponse<GetListOrderDetailListItemDto>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public GetListOrderDetailQueryHandler(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListOrderDetailListItemDto>> Handle(GetListOrderDetailQuery request, CancellationToken cancellationToken)
        {
            IPaginate<OrderDetail> orderDetails = await _orderDetailRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListOrderDetailListItemDto> response = _mapper.Map<GetListResponse<GetListOrderDetailListItemDto>>(orderDetails);
            return response;
        }
    }
}