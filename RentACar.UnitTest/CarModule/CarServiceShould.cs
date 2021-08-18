using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using RentACar.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telerik.JustMock;
using Xunit;

namespace RentACar.UnitTest.CarModule
{
    public class CarServiceShould
    {
        IUnitOfWork unitOfWork { get; set; }
        CarService carService { get; set; }
        public CarServiceShould()
        {
            unitOfWork = Mock.Create<IUnitOfWork>();
            carService = new CarService(unitOfWork);
        }

        private Action<Car, Car> CheckAndUpdateRepositoryMock = (Car existingCar, Car updatedCar) => {
            existingCar.Model = updatedCar.Model;
            existingCar.Year = updatedCar.Year;
            existingCar.Seats = updatedCar.Seats;
            existingCar.Available = updatedCar.Available;
            existingCar.AirConditioner = updatedCar.AirConditioner;
            existingCar.BodyTypeId = updatedCar.BodyTypeId;
            existingCar.BrandId = updatedCar.BrandId;
            existingCar.Gearbox = updatedCar.Gearbox;
            existingCar.Image = updatedCar.Image;
        };

        [Fact]
        public void GetAll_CallsRepositoryGetAllMethod()
        {
            var objectsList = new List<Car>() {
                    new Car(),
                    new Car()
                };

            Mock.Arrange(() => unitOfWork.CarRepository.GetAllWith(Arg.IsAny<bool>(), Arg.IsAny<bool>(), Arg.IsAny<bool>())).Returns(objectsList);

            carService.GetAll(false);

            Mock.Assert(() => unitOfWork.CarRepository.GetAllWith(Arg.IsAny<bool>(), Arg.IsAny<bool>(), Arg.IsAny<bool>()), Occurs.Once());
        }

        [Fact]
        public async void Create_CarIsNull_ThrowException()
        {
            await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => carService.Create(null));
        }

        [Fact]
        public async void Create_CarIsOk_CallsRepositoryAddMethod()
        {
            //Mock.Arrange(() => unitOfWork.CarRepository.Add(Arg.IsAny<Car>())).Returns(Task.FromResult((Car)null));

            var car = new Car();

            await carService.Create(car);

            Mock.Assert(() => unitOfWork.CarRepository.Add(Arg.IsAny<Car>()), Occurs.Exactly(1));
            Mock.Assert(() => unitOfWork.SaveChangesAsync(), Occurs.Once());
        }

        [Fact]
        public async void Update_CarIsNull_ThrowException()
        {
            await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => carService.Update(null));
        }

        [Fact]
        public async void Update_CarIsOkAndHasNoNewImage_ExistingImageRemains()
        {
            string existingImage = "existingImage.jpg";
            Car existingCar = new Car { Image = existingImage };

            Mock.Arrange(() => unitOfWork.CarRepository.GetById(Arg.IsAny<long>())).TaskResult(existingCar);
            Mock.Arrange(() => unitOfWork.CarRepository.CheckAndUpdate(Arg.IsAny<Car>(), Arg.IsAny<Car>())).DoInstead(CheckAndUpdateRepositoryMock).Returns(Task.FromResult((Car)null));

            var car = new Car { Image = null };


            await carService.Update(car);

            Assert.Equal(existingImage, existingCar.Image);
            Mock.Assert(() => unitOfWork.CarRepository.CheckAndUpdate(Arg.IsAny<Car>(), Arg.IsAny<Car>()), Occurs.Once());
            Mock.Assert(() => unitOfWork.SaveChangesAsync(), Occurs.Once());
        }

        [Fact]
        public async void Update_CarIsOkAndHasNewImage_NewImageIsUpdated()
        {
            string existingImage = "existingImage.jpg";
            string newImage = "newImage.jpg";

            Car existingCar = new Car { Image = existingImage };

            Mock.Arrange(() => unitOfWork.CarRepository.GetById(Arg.IsAny<long>())).TaskResult(existingCar);
            Mock.Arrange(() => unitOfWork.CarRepository.CheckAndUpdate(Arg.IsAny<Car>(), Arg.IsAny<Car>())).DoInstead(CheckAndUpdateRepositoryMock).Returns(Task.FromResult((Car)null));

            var car = new Car { Image = newImage };

            await carService.Update(car);

            Assert.Equal(newImage, existingCar.Image);
            Mock.Assert(() => unitOfWork.CarRepository.CheckAndUpdate(Arg.IsAny<Car>(), Arg.IsAny<Car>()), Occurs.Once());
            Mock.Assert(() => unitOfWork.SaveChangesAsync(), Occurs.Once());
        }

        


        [Fact]
        public async void Update_CarDoesNotExists_ThrowException()
        {
            Mock.Arrange(() => unitOfWork.CarRepository.GetById(Arg.IsAny<long>())).TaskResult((Car)null);

            var car = new Car();

            await Assert.ThrowsAsync<NullEntityException>(() => carService.Update(car));
            Mock.Assert(() => unitOfWork.CarRepository.GetById(Arg.IsAny<long>()), Occurs.Once());
        }

        //[Fact]
        //public async void Delete_CarExists_CallsDeleteMethod()
        //{
        //    unitOfWork.Setup(u => u.CarRepository.Delete(It.IsAny<long>()));

        //    await carService.Delete(2);

        //    unitOfWork.Verify(u => u.CarRepository.Delete(It.IsAny<long>()), Times.Once);
        //    unitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        //}
    }
}
