using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork unitOfWork;

        public CarService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Car> GetAll()
        {
            var cars = unitOfWork.CarRepository.GetAll();

            return cars;
        }

        public async Task<Car> GetById(long id) => await unitOfWork.CarRepository.GetById(id);

        public async Task Create(Car car)
        {
            if(car == null)
            {
                throw new ArgumentNotDefinedException();
            }

            car.Available = true;

            await unitOfWork.CarRepository.Add(car);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task Update(Car car)
        {
            if (car == null)
            {
                throw new ArgumentNotDefinedException();
            }


            var existingCar = await unitOfWork.CarRepository.GetById(car.Id);

            if (existingCar == null)
            {
                throw new NullEntityException();
            }

            if (car.Image == null)
            {
                car.Image = existingCar.Image;
            }

            await unitOfWork.CarRepository.CheckAndUpdate(existingCar, car);
            await unitOfWork.SaveChangesAsync();
        }

        //public async Task Delete(long id)
        //{

        //    await unitOfWork.CarRepository.Delete(id);
        //    await unitOfWork.SaveChangesAsync();
        //}
    }
}
