using FluentValidation;

namespace Application.Features.Orders.Commands.Delete;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}