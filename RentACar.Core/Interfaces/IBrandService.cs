using RentACar.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Interfaces
{
    public interface IBrandService
    {
        public IEnumerable<Brand> GetAll();
        public Task Add(Brand brand);
        public Task Update(Brand brand);
    }
}
