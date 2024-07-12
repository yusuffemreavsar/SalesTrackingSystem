using Application.Features.Sales.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using Application.Features.Products.Constants;

namespace Application.Features.Sales.Rules;

public class SaleBusinessRules : BaseBusinessRules
{
    private readonly ISaleRepository _saleRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IProductRepository _productRepository;

    public SaleBusinessRules(ISaleRepository saleRepository, ILocalizationService localizationService, IProductRepository productRepository)
    {
        _saleRepository = saleRepository;
        _localizationService = localizationService;
        _productRepository = productRepository;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, SalesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task SaleShouldExistWhenSelected(Sale? sale)
    {
        if (sale == null)
            await throwBusinessException(SalesBusinessMessages.SaleNotExists);
    }
    public async Task ProductShouldExistWhenSelected(Product? product)
    {
        if (product == null)
            await throwBusinessException(ProductsBusinessMessages.ProductNotExists);
    }

    public async Task SaleIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Sale? sale = await _saleRepository.GetAsync(
            predicate: s => s.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await SaleShouldExistWhenSelected(sale);
    }
    public async Task ProductIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetAsync(
            predicate: s => s.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ProductShouldExistWhenSelected(product);
    }
    public async Task ProductQuantityUpdate(Guid id,int quantity)
    {
        var product = await _productRepository.GetAsync(p => p.Id == id);
        product.StockQuantity = product.StockQuantity - quantity;
        await _productRepository.UpdateAsync(product);
    }
}