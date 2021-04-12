using FluentValidation;

namespace GreenFlux.Application.ChargeStations.Commands.AddChargeStation
{
    public class AddChargeStationCommandValidator : AbstractValidator<AddChargeStationCommand>
    {
        public AddChargeStationCommandValidator()
        {
            RuleFor(v => v.ConnectorMaxCurrent)
                .Must(maxCurrent => maxCurrent > 0)
                .WithMessage("Max current value should be greater than zero");
        }
    }
}