using Application.Features.SalesDetails.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.SalesDetails.Rules;

public class SalesDetailBusinessRules : BaseBusinessRules
{
    private readonly ISalesDetailRepository _salesDetailRepository;
    private readonly ILocalizationService _localizationService;

    public SalesDetailBusinessRules(ISalesDetailRepository salesDetailRepository, ILocalizationService localizationService)
    {
        _salesDetailRepository = salesDetailRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, SalesDetailsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task SalesDetailShouldExistWhenSelected(SalesDetail? salesDetail)
    {
        if (salesDetail == null)
            await throwBusinessException(SalesDetailsBusinessMessages.SalesDetailNotExists);
    }

    public async Task SalesDetailIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        SalesDetail? salesDetail = await _salesDetailRepository.GetAsync(
            predicate: sd => sd.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await SalesDetailShouldExistWhenSelected(salesDetail);
    }
}