using GreenFlux.Application.Common.Interfaces;
using System;

namespace GreenFlux.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
