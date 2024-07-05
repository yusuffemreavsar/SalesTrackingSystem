using Application.Features.Orders.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.Orders.Rules;

public class OrderBusinessRules : BaseBusinessRules
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILocalizationService _localizationService;

    public OrderBusinessRules(IOrderRepository orderRepository, ILocalizationService localizationService)
    {
        _orderRepository = orderRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, OrdersBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task OrderShouldExistWhenSelected(Order? order)
    {
        if (order == null)
            await throwBusinessException(OrdersBusinessMessages.OrderNotExists);
    }

    public async Task OrderIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Order? order = await _orderRepository.GetAsync(
            predicate: o => o.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await OrderShouldExistWhenSelected(order);
    }
}