using FluentValidation;

namespace Application.Features.Orders.Commands.Update;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.CustomerId).NotEmpty();
        RuleFor(c => c.Customer).NotEmpty();
        RuleFor(c => c.ProductId).NotEmpty();
        RuleFor(c => c.Product).NotEmpty();
    }
}