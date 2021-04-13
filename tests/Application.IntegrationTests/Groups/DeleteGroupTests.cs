using System.Threading.Tasks;
using FluentAssertions;
using GreenFlux.Application.Common.Exceptions;
using GreenFlux.Application.Groups.Commands.DeleteGroup;
using GreenFlux.Domain.Entities;
using NUnit.Framework;

namespace GreenFlux.Application.IntegrationTests.Groups
{
    public class DeleteGroupTests : TestBase
    {
        [Test]
        public void ShouldRequireValidGroupId()
        {
            var command = new DeleteGroupCommand {GroupId = 99};

            FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteGroupAndChargeStations()
        {
            var group = await AddGroupAsync();
            var station = await AddChargeStationAsync(group.Id);

            var command = new DeleteGroupCommand
            {
                GroupId = group.Id
            };

            await Testing.SendAsync(command);

            group = await Testing.FindAsync<Group>(command.GroupId);
            group.Should().BeNull();

            var chargeStation = await Testing.FindAsync<ChargeStation>(station.Id);
            chargeStation.Should().BeNull();
        }
    }
}