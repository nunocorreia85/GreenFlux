using System;
using System.Runtime.Serialization;
using AutoMapper;
using GreenFlux.Application.Common.Mappings;
using GreenFlux.Application.Dto;
using GreenFlux.Application.Dto.Queries;
using GreenFlux.Domain.Entities;
using NUnit.Framework;

namespace GreenFlux.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Test]
        [TestCase(typeof(Group), typeof(GroupDto))]
        [TestCase(typeof(ChargeStation), typeof(ChargeStationDto))]
        [TestCase(typeof(Connector), typeof(ConnectorDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            return type.GetConstructor(Type.EmptyTypes) != null ? 
                Activator.CreateInstance(type) : FormatterServices.GetUninitializedObject(type);

            // Type without parameterless constructor
        }
    }
}