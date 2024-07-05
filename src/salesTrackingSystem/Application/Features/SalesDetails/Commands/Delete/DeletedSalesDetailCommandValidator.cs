using FluentValidation;

namespace Application.Features.SalesDetails.Commands.Delete;

public class DeleteSalesDetailCommandValidator : AbstractValidator<DeleteSalesDetailCommand>
{
    public DeleteSalesDetailCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}