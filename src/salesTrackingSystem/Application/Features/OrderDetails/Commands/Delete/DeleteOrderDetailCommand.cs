using Application.Features.OrderDetails.Constants;
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

namespace Application.Features.OrderDetails.Commands.Delete;

public class DeleteOrderDetailCommand : IRequest<DeletedOrderDetailResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, OrderDetailsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetOrderDetails"];

    public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, DeletedOrderDetailResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly OrderDetailBusinessRules _orderDetailBusinessRules;

        public DeleteOrderDetailCommandHandler(IMapper mapper, IOrderDetailRepository orderDetailRepository,
                                         OrderDetailBusinessRules orderDetailBusinessRules)
        {
            _mapper = mapper;
            _orderDetailRepository = orderDetailRepository;
            _orderDetailBusinessRules = orderDetailBusinessRules;
        }

        public async Task<DeletedOrderDetailResponse> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
        {
            OrderDetail? orderDetail = await _orderDetailRepository.GetAsync(predicate: od => od.Id == request.Id, cancellationToken: cancellationToken);
            await _orderDetailBusinessRules.OrderDetailShouldExistWhenSelected(orderDetail);

            await _orderDetailRepository.DeleteAsync(orderDetail!);

            DeletedOrderDetailResponse response = _mapper.Map<DeletedOrderDetailResponse>(orderDetail);
            return response;
        }
    }
}