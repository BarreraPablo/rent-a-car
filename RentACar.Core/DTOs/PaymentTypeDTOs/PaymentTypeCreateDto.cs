using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.DTOs.PaymentTypeDTOs
{
    public class PaymentTypeCreateDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
