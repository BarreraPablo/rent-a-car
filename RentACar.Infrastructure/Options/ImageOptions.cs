using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Options
{
    public class ImageOptions
    {
        public int MaxSize { get; set; }
        public string CarsImagesFolder { get; set; }
        public string AllowedExtensions { get; set; }
    }
}
