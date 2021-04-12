using System.Collections.Generic;
using FluentAssertions;
using GreenFlux.Application.Utils;
using GreenFlux.Domain.Entities;
using NUnit.Framework;

namespace GreenFlux.Application.UnitTests.Utils
{
    public class CombinationsCalculatorTest
    {
        [Test]
        [TestCase(20f, 4)]
        [TestCase(8.9f, 1)]
        [TestCase(10f, 3)]
        [TestCase(5f, 0)]
        public void ShouldGetAllPossibleCombinationsOfConnectors(float targetCurrent, int expectedNumberOfCombinations)
        {
            var connectorsList = CombinationsCalculator.GetCombinations(new List<Connector>
            {
                new() {MaxCurrent = 44, ChargeStationId = 1},
                new() {MaxCurrent = 10, ChargeStationId = 1},
                new() {MaxCurrent = 8.9f, ChargeStationId = 2},
                new() {MaxCurrent = 1.1f, ChargeStationId = 2},
                new() {MaxCurrent = 20, ChargeStationId = 3},
                new() {MaxCurrent = 30, ChargeStationId = 3},
                new() {MaxCurrent = 10, ChargeStationId = 4}
            }, targetCurrent);

            connectorsList.Count.Should().Be(expectedNumberOfCombinations);
        }
    }
}