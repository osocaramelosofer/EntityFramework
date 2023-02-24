using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Controllers;
using API.Dtos;
using API.Repository.Interfaces;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;

namespace Tests.MachineStatusCatalogControllerTests
{
    public class MachineControllerTests
    {
        private readonly  IMachineStatusCatalog _ctMachine;
        private readonly IMapper _mapper;

        public MachineControllerTests()
        {
            _ctMachine = A.Fake<IMachineStatusCatalog>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void MachineController_GetMachines_retunOk()
        {
            // Arrange
            var machines = A.Fake<ICollection<MachineStatusCatalogDto>>();
            var machineList = A.Fake<List<MachineStatusCatalogDto>>();
            A.CallTo(() => _mapper.Map<List<MachineStatusCatalogDto>>(machines)).Returns(machineList);
            var controller = new MachineStatusCatalogsController(_ctMachine, _mapper);

            // Act
            var result = controller.GetMachineStatusCatalogs();

            // Assert
            result.Should().NotBeNull();
        }
    }
}
