using FluentValidation;

namespace Application.Features.OrderDetails.Commands.Delete;

public class DeleteOrderDetailCommandValidator : AbstractValidator<DeleteOrderDetailCommand>
{
    public DeleteOrderDetailCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}