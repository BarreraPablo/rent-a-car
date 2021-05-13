using Microsoft.AspNetCore.Http;
using RentACar.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveImage(IFormFile image);
        void PrependUrl(IEnumerable<Car> cars, string baseUrl);
        void PrependUrl(Car car, string baseUrl);
    }
}