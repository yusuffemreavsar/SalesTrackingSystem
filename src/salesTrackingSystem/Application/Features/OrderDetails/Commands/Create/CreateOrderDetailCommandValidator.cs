using FluentValidation;

namespace Application.Features.OrderDetails.Commands.Create;

public class CreateOrderDetailCommandValidator : AbstractValidator<CreateOrderDetailCommand>
{
    public CreateOrderDetailCommandValidator()
    {
        RuleFor(c => c.OrderId).NotEmpty();
        RuleFor(c => c.Order).NotEmpty();
        RuleFor(c => c.ProductName).NotEmpty();
        RuleFor(c => c.Quantity).NotEmpty();
        RuleFor(c => c.UnitPrice).NotEmpty();
        RuleFor(c => c.TotalPrice).NotEmpty();
    }
}