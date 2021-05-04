using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.DTOs.PaymentTypeDTOs
{
    public class PaymentTypeReadDto
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
