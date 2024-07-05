using Application.Features.OrderDetails.Constants;
using Application.Features.OrderDetails.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.OrderDetails.Constants.OrderDetailsOperationClaims;

namespace Application.Features.OrderDetails.Commands.Create;

public class CreateOrderDetailCommand : IRequest<CreatedOrderDetailResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }

    public string[] Roles => [Admin, Write, OrderDetailsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetOrderDetails"];

    public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand, CreatedOrderDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly OrderDetailBusinessRules _orderDetailBusinessRules;

        public CreateOrderDetailCommandHandler(IMapper mapper, IOrderDetailRepository orderDetailRepository,
                                         OrderDetailBusinessRules orderDetailBusinessRules)
        {
            _mapper = mapper;
            _orderDetailRepository = orderDetailRepository;
            _orderDetailBusinessRules = orderDetailBusinessRules;
        }

        public async Task<CreatedOrderDetailResponse> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            OrderDetail orderDetail = _mapper.Map<OrderDetail>(request);

            await _orderDetailRepository.AddAsync(orderDetail);

            CreatedOrderDetailResponse response = _mapper.Map<CreatedOrderDetailResponse>(orderDetail);
            return response;
        }
    }
}