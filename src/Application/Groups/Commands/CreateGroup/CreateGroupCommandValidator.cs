using FluentValidation;

namespace GreenFlux.Application.Groups.Commands.CreateGroup
{
    public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupCommandValidator()
        {
            RuleFor(v => v.Capacity)
                .Must(c => c > 0)
                .WithMessage("Capacity should be greater than zero");
        }
    }
}