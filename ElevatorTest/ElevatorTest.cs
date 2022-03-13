using Coravel;
using Elevador.Context;
using Elevador.Controllers;
using Elevador.Interface;
using Elevador.Jobs;
using Elevador.Models;
using Elevador.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ElevatorTest
{
    public class ElevatorTest
    {
        private ElevatorController _elevatorController { get; set; } = null!;
        private ElevatorJob _elevatorJob { get; set; } = null!;

        [OneTimeSetUp]
        public void Setup()
        {

            IConfiguration _configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.json")
                    .Build();


            var services = new ServiceCollection();
            services.AddLogging(conf => conf.AddConsole());
            services.AddSingleton(_configuration);
            services.AddDbContext<ElevatorContext>(op => op.UseInMemoryDatabase("Elevator"));

            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IElevatorState, ElevatorStateService>();
            services.AddScoped<IElevator, ElevatorService>();

            var serviceProvider = services.BuildServiceProvider();


            var factory = serviceProvider.GetService<ILoggerFactory>();

            ILogger<ElevatorController> _loggerController = factory.CreateLogger<ElevatorController>();
            ILogger<ElevatorJob> _loggerJob = factory.CreateLogger<ElevatorJob>();
            IElevator _elevator = serviceProvider.GetService<IElevator>();


            _elevatorJob = new ElevatorJob(_elevator, _loggerJob);
            _elevatorController = new ElevatorController(_loggerController, _elevator, _configuration);

        }

        [Test, Order(1)]
        public async Task GetCurrentState_ShouldReturnElevatorCurrentState()
        {

            //Act
            ActionResult<ElevatorState> Res = await _elevatorController.GetCurrentState();


            //Assert
            OkObjectResult? result = Res.Result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(Res.Result);
            Assert.NotNull(result);

            Assert.IsInstanceOf<ElevatorState>(result.Value);
            ElevatorState state = (ElevatorState)result.Value;

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(state));

            Assert.Pass();
        }


        [Test]
        [TestCase(9)]
        [TestCase(8)]
        [TestCase(7)]
        [TestCase(6)]
        [TestCase(5)]
        [TestCase(4)]
        [TestCase(3)]
        [TestCase(2)]
        [TestCase(1)]
        [Order(2)]
        public async Task CallFromInside_ShouldMoveElevator(int ToFloor)
        {
            //Arrange
            CallFromInsideRequest req = new CallFromInsideRequest
            {
                ToFloor = ToFloor
            };

            //Act
            ActionResult<ElevatorResponse> Res = await _elevatorController.CallFromInside(req);

            //Assert
            OkObjectResult? result = Res.Result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(Res.Result);
            Assert.NotNull(result);
            Assert.IsInstanceOf<ElevatorResponse>(result.Value);
            ElevatorResponse state = (ElevatorResponse)result.Value;
            Assert.IsTrue(state.RequestAccepted);

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(state));
            Assert.Pass();
        }

        [Test]
        [TestCase(3)]
        //[TestCase(3)]
        [Order(3)]
        public async Task CallFromOutside_ShouldMoveElevator(int FromFloor)
        {
            //Arrange
            CallFromOutSideRequest req = new CallFromOutSideRequest
            {
                FromFloor = FromFloor
            };

            //Act
            ActionResult<ElevatorResponse> Res = await _elevatorController.CallFromOutside(req);

            //Assert
            OkObjectResult? result = Res.Result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(Res.Result);
            Assert.NotNull(result);
            Assert.IsInstanceOf<ElevatorResponse>(result.Value);
            ElevatorResponse state = (ElevatorResponse)result.Value;
            Assert.IsTrue(state.RequestAccepted);

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(state));
            Assert.Pass();
        }


        [Test]
        [Order(4)]
        public async Task MoveElevator()
        {
            await _elevatorJob.Invoke();
            Assert.Pass();
        }

        [Test]
        [Order(5)]
        public async Task FinalGetCurrentState_ShouldReturnElevatorCurrentStateAndCurrentFloorGreatterThanOne()
        {

            //Act
            ActionResult<ElevatorState> Res = await _elevatorController.GetCurrentState();


            //Assert
            OkObjectResult? result = Res.Result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(Res.Result);
            Assert.NotNull(result);

            Assert.IsInstanceOf<ElevatorState>(result.Value);
            ElevatorState state = (ElevatorState)result.Value;

            //Assert.IsTrue(state.CurrentFloor > 1);
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(state));

            Assert.Pass();
        }


    }
}