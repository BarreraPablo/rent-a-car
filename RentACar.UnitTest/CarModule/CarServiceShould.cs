using Moq;
using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using RentACar.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RentACar.UnitTest.CarModule
{
    public class CarServiceShould
    {
        Mock<IUnitOfWork> unitOfWork { get; set; }
        CarService carService { get; set; }
        public CarServiceShould()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            carService = new CarService(unitOfWork.Object);
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


            unitOfWork.Setup(m => m.CarRepository.GetAllWith(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>())).Returns(objectsList);

            carService.GetAll(false);

            unitOfWork.Verify(u => u.CarRepository.GetAllWith(It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [Fact]
        public async void Create_CarIsNull_ThrowException()
        {
            await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => carService.Create(null));
        }

        [Fact]
        public async void Create_CarIsOk_CallsRepositoryAddMethod()
        {
            unitOfWork.Setup(u => u.CarRepository.Add(It.IsAny<Car>()));

            var car = new Car();

            await carService.Create(car);

            unitOfWork.Verify(u => u.CarRepository.Add(It.IsAny<Car>()), Times.Once);
            unitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
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

            unitOfWork.Setup(u => u.CarRepository.GetById(It.IsAny<long>())).ReturnsAsync(existingCar);
            unitOfWork.Setup(u => u.CarRepository.CheckAndUpdate(It.IsAny<Car>(), It.IsAny<Car>())).Callback(CheckAndUpdateRepositoryMock);

            var car = new Car { Image = null };


            await carService.Update(car);

            Assert.Equal(existingImage, existingCar.Image);
            unitOfWork.Verify(u => u.CarRepository.CheckAndUpdate(It.IsAny<Car>(), It.IsAny<Car>()), Times.Once);
            unitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async void Update_CarIsOkAndHasNewImage_NewImageIsUpdated()
        {
            string existingImage = "existingImage.jpg";
            string newImage = "newImage.jpg";

            Car existingCar = new Car { Image = existingImage };

            unitOfWork.Setup(u => u.CarRepository.GetById(It.IsAny<long>())).ReturnsAsync(existingCar);
            unitOfWork.Setup(u => u.CarRepository.CheckAndUpdate(It.IsAny<Car>(), It.IsAny<Car>())).Callback(CheckAndUpdateRepositoryMock);

            var car = new Car { Image = newImage };

            await carService.Update(car);

            Assert.Equal(newImage, existingCar.Image);
            unitOfWork.Verify(u => u.CarRepository.CheckAndUpdate(It.IsAny<Car>(), It.IsAny<Car>()), Times.Once);
            unitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        


        [Fact]
        public async void Update_CarDoesNotExists_ThrowException()
        {
            unitOfWork.Setup(u => u.CarRepository.GetById(It.IsAny<long>())).ReturnsAsync((Car)null);

            var car = new Car();

            await Assert.ThrowsAsync<NullEntityException>(() => carService.Update(car));
            unitOfWork.Verify(u => u.CarRepository.GetById(It.IsAny<long>()), Times.Once);
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
