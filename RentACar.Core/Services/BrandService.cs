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
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task Add(Brand brand)
        {
            if(brand == null)
            {
                throw new ArgumentNotDefinedException();
            }

            await unitOfWork.BrandRepository.Add(brand);
            await unitOfWork.SaveChangesAsync();
        }

        public IEnumerable<Brand> GetAll()
        {
            return unitOfWork.BrandRepository.GetAll();
        }

        public async Task Update(Brand brand) {
            if (brand == null)
            {
                throw new ArgumentNotDefinedException();
            }

            await unitOfWork.BrandRepository.GetAndUpdate(brand);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
