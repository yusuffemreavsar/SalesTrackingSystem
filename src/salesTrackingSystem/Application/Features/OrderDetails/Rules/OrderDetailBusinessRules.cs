using Application.Features.OrderDetails.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.OrderDetails.Rules;

public class OrderDetailBusinessRules : BaseBusinessRules
{
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly ILocalizationService _localizationService;

    public OrderDetailBusinessRules(IOrderDetailRepository orderDetailRepository, ILocalizationService localizationService)
    {
        _orderDetailRepository = orderDetailRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, OrderDetailsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task OrderDetailShouldExistWhenSelected(OrderDetail? orderDetail)
    {
        if (orderDetail == null)
            await throwBusinessException(OrderDetailsBusinessMessages.OrderDetailNotExists);
    }

    public async Task OrderDetailIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        OrderDetail? orderDetail = await _orderDetailRepository.GetAsync(
            predicate: od => od.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await OrderDetailShouldExistWhenSelected(orderDetail);
    }
}