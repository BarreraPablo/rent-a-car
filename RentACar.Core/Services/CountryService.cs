using RentACar.Core.Entities;
using RentACar.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork unitOfWork;

        public CountryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Country> GetAll() => unitOfWork.CountryRepository.GetAll();

        public async Task Create(Country country)
        {
            if (country == null)
            {
                throw new ArgumentNullException();
            }

            Country existentCountry = await unitOfWork.CountryRepository.GetByName(country.Name);

            if(existentCountry == null)
            {
                await unitOfWork.CountryRepository.Add(country);
                await unitOfWork.SaveChangesAsync();
            } else
            {
                country.Id = existentCountry.Id;
                country.Name = existentCountry.Name;
                country.CreatedAt = existentCountry.CreatedAt;
                country.ModifiedAt = existentCountry.ModifiedAt;
            }

        }

        public async Task Update(Country country)
        {
            if (country == null)
            {
                throw new ArgumentNullException();
            }

            await unitOfWork.CountryRepository.GetAndUpdate(country);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
