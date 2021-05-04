using Microsoft.EntityFrameworkCore;
using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Infrastructure.Data;
using RentACar.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RentACar.UnitTest.CarModule
{
    [Collection("Repository tests")]
    public class CarRepositoryShould
    {
        protected DbContextOptions<RentACarContext> ContextOptions { get; }

        public CarRepositoryShould()
        {
            ContextOptions = new DbContextOptionsBuilder<RentACarContext>()
            .UseInMemoryDatabase("RentACar")
            .Options;

            Seed();
        }

        private void Seed()
        {
            var car1 = new Car { Id = 1, Model = "Uno", Year = 2000, Gearbox = "Automatic", AirConditioner = false, Available = true, BodyTypeId = 2, BrandId = 4, Image = "img.jpg", Doors = 4, Seats = 5, CreatedAt = DateTime.Parse("2021-05-04") };
            var car2 = new Car { Id = 2, Model = "Fiesta", Year = 2016, Gearbox = "Manual", AirConditioner = true, Available = true, BodyTypeId = 1, BrandId = 5, Image = "img2.jpg", Doors = 4, Seats = 5, CreatedAt = DateTime.Parse("2021-05-01") };
            var car3 = new Car { Id = 3, Model = "Focus", Year = 2019, Gearbox = "Manual", AirConditioner = true, Available = true, BodyTypeId = 1, BrandId = 6, Image = "img3.jpg", Doors = 4, Seats = 5, CreatedAt = DateTime.Parse("2021-05-05") };

            var brand4 = new Brand { Id = 4, Name = "Brand4" };
            var brand5 = new Brand { Id = 5, Name = "Brand5" };
            var brand6 = new Brand { Id = 6, Name = "Brand6" };

            using (var context = new RentACarContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var unitOfWork = new UnitOfWork(context);

                unitOfWork.CarRepository.Add(car1);
                unitOfWork.CarRepository.Add(car2);
                unitOfWork.CarRepository.Add(car3);

                unitOfWork.BrandRepository.Add(brand4);
                unitOfWork.BrandRepository.Add(brand5);
                unitOfWork.BrandRepository.Add(brand6);

                unitOfWork.SaveChanges();
            }
        }

        [Fact]
        public void GetAll_ReturnsThreeCarsStoredInDB()
        {
            using (var context = new RentACarContext(ContextOptions)) {
                var unitOfWork = new UnitOfWork(context);

                var cars = unitOfWork.CarRepository.GetAll();

                Assert.Equal(3, cars.Count());
            }
        }

        [Fact]
        public async void Add_CarIsOk_CarIsRegistered()
        {
            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);

                Car car = new Car()
                {
                    Id = 4,
                    BrandId = 4,
                    AirConditioner = true,
                    Gearbox = "Automatic",
                    Doors = 4,
                    Year = 2020,
                    BodyTypeId = 2,
                    Image = "img4.jpg",
                    Model = "Vento",
                    Seats = 5,

                };

                await unitOfWork.CarRepository.Add(car);
                unitOfWork.SaveChanges();


                Assert.Equal(4, car.Id); // After the insert the entity must have id > 0
            }

            using (var context = new RentACarContext(ContextOptions))
            {
                var car = context.Set<Car>().FirstOrDefault(u => u.Model == "Vento");

                Assert.Equal(4, car.Id);
                Assert.Equal(4, car.BrandId);
                Assert.True(car.AirConditioner);
                Assert.Equal("Vento", car.Model);
                Assert.Equal("Automatic", car.Gearbox);
                Assert.Equal(2020, car.Year);
                Assert.Equal("img4.jpg", car.Image);
                Assert.Equal(DateTime.Today.Date, car.CreatedAt.Date);
            }
        }

        [Fact]
        public async void Add_CarObjectIsNull_ThrowException()
        {
            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);

                await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => unitOfWork.CarRepository.Add(null));
            }
        }

        [Fact]
        public async void GetAndUpdate_CarIsOk_UpdatesCarInDB()
        {
            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);

                var car = new Car { Id = 3, Model = "Focus", Year = 2020, Gearbox = "Automatic", AirConditioner = true, Available = true, BodyTypeId = 1, BrandId = 6, Image = "img4.jpg", Doors = 4, Seats = 5, CreatedAt = DateTime.Parse("2021-05-05") };

                await unitOfWork.CarRepository.GetAndUpdate(car);
                await unitOfWork.SaveChangesAsync();
            }

            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);
                var car = await unitOfWork.CarRepository.GetById(3);

                Assert.Equal(3, car.Id);
                Assert.Equal("Focus", car.Model);
                Assert.Equal("Automatic", car.Gearbox);
                Assert.Equal("img4.jpg", car.Image);
                Assert.Equal(5, (int)car.Seats);
            }
        }

        [Fact]
        public async void GetAndUpdate_CarIsNull_ThrowsException()
        {
            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);

                await Assert.ThrowsAsync<ArgumentNotDefinedException>(() => unitOfWork.CarRepository.GetAndUpdate(null));
            }
        }

        [Fact]
        public async void GetAndUpdate_CarDoesNotExists_ThrowsException()
        {
            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);

                var car = new Car { Id = 5, Model = "Focus", Year = 2020, Gearbox = "Automatic", AirConditioner = true, Available = true, BodyTypeId = 1, BrandId = 6, Image = "img4.jpg", Doors = 4, Seats = 5, CreatedAt = DateTime.Parse("2021-05-05") };

                await Assert.ThrowsAsync<NullEntityException>(() => unitOfWork.CarRepository.GetAndUpdate(car));
            }
        }

        [Fact]
        public async void CheckAndUpdate_carAndCardWithChangesAreOk_SaveChanges() {
            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);

                Car carWithChanges = new Car { Id = 3, Model = "Focus", Year = 2020, Gearbox = "Automatic", AirConditioner = true, Available = true, BodyTypeId = 1, BrandId = 6, Image = "img4.jpg", Doors = 4, Seats = 5, CreatedAt = DateTime.Parse("2021-05-05") };
                Car existingCar = await unitOfWork.CarRepository.GetById(carWithChanges.Id);

                await unitOfWork.CarRepository.CheckAndUpdate(existingCar, carWithChanges);
                await unitOfWork.SaveChangesAsync();
            }

            using (var context = new RentACarContext(ContextOptions))
            {
                var unitOfWork = new UnitOfWork(context);
                var car = await unitOfWork.CarRepository.GetById(3);

                Assert.Equal(3, car.Id);
                Assert.Equal("Focus", car.Model);
                Assert.Equal("Automatic", car.Gearbox);
                Assert.Equal("img4.jpg", car.Image);
                Assert.Equal(5, (int)car.Seats);
            }

        }

    }
}
