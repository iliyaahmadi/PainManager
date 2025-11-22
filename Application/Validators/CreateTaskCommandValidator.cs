using FluentValidation;
using Application.Commands;

namespace Application.Validators;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description must be less than 500 characters.");
    }
}