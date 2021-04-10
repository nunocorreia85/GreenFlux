using System;
using GreenFlux.Application.Common.Interfaces;

namespace GreenFlux.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}