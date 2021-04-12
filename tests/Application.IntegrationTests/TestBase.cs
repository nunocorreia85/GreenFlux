using System.Threading.Tasks;
using GreenFlux.Domain.Entities;
using NUnit.Framework;

namespace GreenFlux.Application.IntegrationTests
{
    using static Testing;

    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }

        protected static async Task<Connector> AddConnectorAsync(long chargeStationId, int connectorId, float maxCurrent)
        {
            var connector = new Connector
            {
                Id = connectorId,
                ChargeStationId = chargeStationId,
                MaxCurrent = maxCurrent,
            };

            await AddAsync(connector);
            return connector;
        }

        protected static async Task<Group> AddGroupAsync()
        {
            var group = new Group()
            {
                Name = "G1",
                Capacity = 100,
            };

            await AddAsync(@group);
            return @group;
        }

        protected static async Task<ChargeStation> AddChargeStationAsync(long groupId)
        {
            var chargeStation = new ChargeStation
            {
                Name = "S1",
                GroupId = groupId
            };
            await AddAsync(chargeStation);
            return chargeStation;
        }
    }
}