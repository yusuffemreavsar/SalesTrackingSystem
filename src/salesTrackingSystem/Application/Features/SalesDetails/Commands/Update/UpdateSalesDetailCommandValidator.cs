using FluentValidation;

namespace Application.Features.SalesDetails.Commands.Update;

public class UpdateSalesDetailCommandValidator : AbstractValidator<UpdateSalesDetailCommand>
{
    public UpdateSalesDetailCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.SaleId).NotEmpty();
        RuleFor(c => c.Sale).NotEmpty();
        RuleFor(c => c.ProductSale).NotEmpty();
        RuleFor(c => c.Product).NotEmpty();
        RuleFor(c => c.Quantity).NotEmpty();
    }
}