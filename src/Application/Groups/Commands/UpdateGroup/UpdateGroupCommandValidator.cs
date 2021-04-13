using FluentValidation;

namespace GreenFlux.Application.Groups.Commands.UpdateGroup
{
    public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupCommandValidator()
        {
            RuleFor(v => v.Capacity)
                .Must(c => c > 0)
                .WithMessage("Capacity should be greater than zero");
        }
    }
}