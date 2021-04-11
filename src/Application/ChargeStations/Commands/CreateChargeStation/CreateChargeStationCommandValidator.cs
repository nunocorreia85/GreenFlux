﻿using FluentValidation;

namespace GreenFlux.Application.ChargeStations.Commands.CreateChargeStation
{
    public class CreateChargeStationCommandValidator : AbstractValidator<CreateChargeStationCommand>
    {
        public CreateChargeStationCommandValidator()
        {
            RuleFor(v => v.Connectors)
                .NotEmpty();
            RuleFor(v => v.Connectors)
                .Must(dtos => dtos.Count < 5)
                .WithMessage("No more than 5 connectors is allowed.");
            RuleFor(v => v.Connectors)
                .ForEach(c => c.Must(dto => dto.MaxCurrent > 0)
                    .WithMessage("Max current value should be greater than zero"));
        }
    }
}