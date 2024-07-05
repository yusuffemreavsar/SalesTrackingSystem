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

namespace Application.Features.OrderDetails.Commands.Update;

public class UpdateOrderDetailCommand : IRequest<UpdatedOrderDetailResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }

    public string[] Roles => [Admin, Write, OrderDetailsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetOrderDetails"];

    public class UpdateOrderDetailCommandHandler : IRequestHandler<UpdateOrderDetailCommand, UpdatedOrderDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly OrderDetailBusinessRules _orderDetailBusinessRules;

        public UpdateOrderDetailCommandHandler(IMapper mapper, IOrderDetailRepository orderDetailRepository,
                                         OrderDetailBusinessRules orderDetailBusinessRules)
        {
            _mapper = mapper;
            _orderDetailRepository = orderDetailRepository;
            _orderDetailBusinessRules = orderDetailBusinessRules;
        }

        public async Task<UpdatedOrderDetailResponse> Handle(UpdateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            OrderDetail? orderDetail = await _orderDetailRepository.GetAsync(predicate: od => od.Id == request.Id, cancellationToken: cancellationToken);
            await _orderDetailBusinessRules.OrderDetailShouldExistWhenSelected(orderDetail);
            orderDetail = _mapper.Map(request, orderDetail);

            await _orderDetailRepository.UpdateAsync(orderDetail!);

            UpdatedOrderDetailResponse response = _mapper.Map<UpdatedOrderDetailResponse>(orderDetail);
            return response;
        }
    }
}