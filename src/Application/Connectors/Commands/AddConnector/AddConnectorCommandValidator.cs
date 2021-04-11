using FluentValidation;

namespace GreenFlux.Application.Connectors.Commands.AddConnector
{
    public class AddConnectorCommandValidator : AbstractValidator<AddConnectorCommand>
    {
        public AddConnectorCommandValidator()
        {
            RuleFor(v => v.MaxCurrent)
                .Must(f => f > 0)
                .WithMessage("Max current value should be greater than zero");
        }
    }
}