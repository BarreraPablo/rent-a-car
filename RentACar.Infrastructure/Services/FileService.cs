using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RentACar.Core.Entities;
using RentACar.Infrastructure.Interfaces;
using RentACar.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment env;
        private readonly ImageOptions options;

        public FileService(IWebHostEnvironment env, IOptions<ImageOptions> options)
        {
            this.env = env;
            this.options = options.Value;
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            var extension = System.IO.Path.GetExtension(image.FileName);
            var allowedExtensions = options.AllowedExtensions.Split(',');

            if(!Array.Exists(allowedExtensions, e => e == extension)) {
                throw new FormatException();
            }

            var fileName = Guid.NewGuid().ToString() + extension;

            var filePath = System.IO.Path.Combine(env.ContentRootPath, options.CarsImagesFolder, fileName);

            if (image.Length > options.MaxSize)
            {
                throw new FormatException();
            }


            using (System.IO.Stream fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create)) {
                await image.CopyToAsync(fileStream);
            }

            return fileName;
        }

        public void PrependUrl(IEnumerable<Car> cars, string baseUrl)
        {
            var imagesFolder = options.CarsImagesFolder.Replace("wwwroot", "");
            foreach (var car in cars)
            {
                if(car.Image != null)
                {
                    var filePath = baseUrl + imagesFolder + car.Image;

                    car.Image = filePath;
                }
            }
        }
    }
}
