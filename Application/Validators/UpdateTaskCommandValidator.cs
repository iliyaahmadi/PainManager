using FluentValidation;
using Application.Commands;

namespace Application.Validators;

public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        RuleFor(x => x.Description).Length(10, 500).WithMessage("Description must be between 10 and 500 characters.");
    }
}