using Application.Features.Orders.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Orders.Constants.OrdersOperationClaims;

namespace Application.Features.Orders.Queries.GetList;

public class GetListOrderQuery : IRequest<GetListResponse<GetListOrderListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListOrders({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetOrders";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListOrderQueryHandler : IRequestHandler<GetListOrderQuery, GetListResponse<GetListOrderListItemDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetListOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListOrderListItemDto>> Handle(GetListOrderQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Order> orders = await _orderRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListOrderListItemDto> response = _mapper.Map<GetListResponse<GetListOrderListItemDto>>(orders);
            return response;
        }
    }
}