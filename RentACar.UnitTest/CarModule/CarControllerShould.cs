using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Api.Controllers;
using RentACar.Core.DTOs.CarDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using RentACar.Infrastructure.Interfaces;
using RentACar.Infrastructure.Mappings;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telerik.JustMock;
using Xunit;

namespace RentACar.UnitTest.CarModule
{
    public class CarControllerShould
    {
        CarController carController;
        ICarService carService;
        IFileService fileService;
        IMapper mapper;

        public CarControllerShould()
        {
            carService = Mock.Create<ICarService>();
            fileService = Mock.Create<IFileService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
            });
            mapper = config.CreateMapper();

            carController = new CarController(mapper, carService, fileService);
        }

        [Fact]

        public async Task Create__carCreateDtoIsOkImageIsNull__ReturnsNoContent()
        {
            CarCreateDto carCreateDto = new()
            {
                
            };

            var result = await carController.Create(carCreateDto, null);

            Mock.Assert(() => carService.Create(Arg.IsAny<Car>()), Occurs.Once());
            Mock.Assert(() => fileService.SaveImage(Arg.IsAny<IFormFile>()), Occurs.Never());
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Create__carCreateDtoIsOkImageIsOk__ReturnsNoContent()
        {
            CarCreateDto carCreateDto = new();

            var file = Mock.Create<IFormFile>();

            string imageName = "test.png";

            Mock.Arrange(() => fileService.SaveImage(Arg.IsAny<IFormFile>())).TaskResult(imageName);

            var result = await carController.Create(carCreateDto, file);

            Mock.Assert(() => carService.Create(Arg.Matches<Car>(c => c.Image == imageName)), Occurs.Once());
            Mock.Assert(() => fileService.SaveImage(Arg.IsAny<IFormFile>()), Occurs.Once());
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]

        public async Task Update__carCreateDtoIsOkImageIsNull__ReturnsNoContent()
        {
            CarUpdateDto carUpdateDto = new()
            {

            };

            var result = await carController.UpdateCar(carUpdateDto, null);

            Mock.Assert(() => carService.Update(Arg.IsAny<Car>()), Occurs.Once());
            Mock.Assert(() => fileService.SaveImage(Arg.IsAny<IFormFile>()), Occurs.Never());
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update__carCreateDtoIsOkImageIsOk__ReturnsNoContent()
        {
            CarUpdateDto carUpdateDto = new();

            var file = Mock.Create<IFormFile>();

            string imageName = "test.png";

            Mock.Arrange(() => fileService.SaveImage(Arg.IsAny<IFormFile>())).TaskResult(imageName);

            var result = await carController.UpdateCar(carUpdateDto, file);

            Mock.Assert(() => carService.Update(Arg.Matches<Car>(c => c.Image == imageName)), Occurs.Once());
            Mock.Assert(() => fileService.SaveImage(Arg.IsAny<IFormFile>()), Occurs.Once());
            Assert.IsType<NoContentResult>(result);
        }

        //[Fact]
        //public async Task GetAll__OnlyAvailable__ReturnsOkWithCars()
        //{
        //    List<Car> cars = new List<Car>()
        //    {
        //        new Car {},
        //        new Car {},
        //        new Car {}
        //    };
        //    List<CarReadDto> carsReadDto = new()
        //    {
        //        new CarReadDto(),
        //        new CarReadDto(),
        //        new CarReadDto()
        //    };

        //    Mock.Arrange(() => carService.GetAll(Arg.IsAny<bool>())).Returns(cars);
        //    Mock.Arrange(() => fileService.PrependUrl(Arg.IsAny<IEnumerable<Car>>(), Arg.AnyString));

        //    var result = await carController.GetAll(true);

        //    Mock.Assert(() => carService.GetAll(Arg.IsAny<bool>()), Occurs.Once());
        //    Mock.Assert(() => fileService.PrependUrl(Arg.IsAny<IEnumerable<Car>>(), Arg.AnyString), Occurs.Once());
        //}
    }
}
