using FluentValidation;

namespace Application.Features.SalesDetails.Commands.Create;

public class CreateSalesDetailCommandValidator : AbstractValidator<CreateSalesDetailCommand>
{
    public CreateSalesDetailCommandValidator()
    {
        RuleFor(c => c.SaleId).NotEmpty();
        RuleFor(c => c.Sale).NotEmpty();
        RuleFor(c => c.ProductSale).NotEmpty();
        RuleFor(c => c.Product).NotEmpty();
        RuleFor(c => c.Quantity).NotEmpty();
    }
}