using Application.Features.Orders.Constants;
using Application.Features.Orders.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Orders.Constants.OrdersOperationClaims;

namespace Application.Features.Orders.Queries.GetById;

public class GetByIdOrderQuery : IRequest<GetByIdOrderResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdOrderQueryHandler : IRequestHandler<GetByIdOrderQuery, GetByIdOrderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderBusinessRules _orderBusinessRules;

        public GetByIdOrderQueryHandler(IMapper mapper, IOrderRepository orderRepository, OrderBusinessRules orderBusinessRules)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderBusinessRules = orderBusinessRules;
        }

        public async Task<GetByIdOrderResponse> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
        {
            Order? order = await _orderRepository.GetAsync(predicate: o => o.Id == request.Id, cancellationToken: cancellationToken);
            await _orderBusinessRules.OrderShouldExistWhenSelected(order);

            GetByIdOrderResponse response = _mapper.Map<GetByIdOrderResponse>(order);
            return response;
        }
    }
}