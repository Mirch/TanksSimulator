using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TanksSimulator.Shared.Models;
using TanksSimulator.WebApi.Controllers.Maps;
using TanksSimulator.WebApi.Data;
using Xunit;

namespace TanksSimulator.WebApi.Tests.Controllers
{
    public class MapsControllerTests
    {
        public class GetAll
        {
            [Fact]
            public async void ReturnsOkResult_WithListOfMaps()
            {
                var controller = BuildController();

                var result = await controller.GetAll() as OkObjectResult;
                Assert.NotNull(result);

                var value = result.Value as IEnumerable<GameMapModel>;
                Assert.NotNull(value);
                Assert.NotEmpty(value);
            }
        }

        public class GetById
        {
            [Fact]
            public async void ReturnsNotFoundResult_WhenIdIsNotInTheRepository()
            {
                var controller = BuildController();

                var result = await controller.GetById("notinthedb") as NotFoundObjectResult;
                Assert.NotNull(result);
            }

            [Fact]
            public async void ReturnsOkResultWithRequestedMap_WhenIdExistsInTheRepository()
            {
                var controller = BuildController();

                var result = await controller.GetById("1") as OkObjectResult;
                Assert.NotNull(result);

                var value = result.Value as GameMapModel;
                Assert.NotNull(value);
                Assert.Equal("1", value.Id);
                Assert.Equal(10, value.Size);
            }
        }

        public static MapsController BuildController()
        {
            var repository = new Mock<IRepository<GameMapModel>>();
            repository.Setup(x => x.GetAsync())
                .Returns(Task.FromResult(
                    new List<GameMapModel> {
                        new GameMapModel { Id = "1", Size = 10 },
                        new GameMapModel { Id = "2", Size = 10 },
                    }));

            repository.Setup(x => x.GetByIdAsync(It.Is<string>(s => s == "1")))
                .Returns(Task.FromResult(
                        new GameMapModel { Id = "1", Size = 10 }));

            var controller = new MapsController(
                repository.Object);

            return controller;
        }
    }
}
