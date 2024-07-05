using Application.Features.Orders.Constants;
using Application.Features.Orders.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Orders.Constants.OrdersOperationClaims;

namespace Application.Features.Orders.Commands.Update;

public class UpdateOrderCommand : IRequest<UpdatedOrderResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public string[] Roles => [Admin, Write, OrdersOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetOrders"];

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, UpdatedOrderResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly OrderBusinessRules _orderBusinessRules;

        public UpdateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository,
                                         OrderBusinessRules orderBusinessRules)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _orderBusinessRules = orderBusinessRules;
        }

        public async Task<UpdatedOrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            Order? order = await _orderRepository.GetAsync(predicate: o => o.Id == request.Id, cancellationToken: cancellationToken);
            await _orderBusinessRules.OrderShouldExistWhenSelected(order);
            order = _mapper.Map(request, order);

            await _orderRepository.UpdateAsync(order!);

            UpdatedOrderResponse response = _mapper.Map<UpdatedOrderResponse>(order);
            return response;
        }
    }
}