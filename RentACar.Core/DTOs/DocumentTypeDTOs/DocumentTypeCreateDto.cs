using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.DTOs.DocumentTypeDTOs
{
    public class DocumentTypeCreateDto
    {
        [Required(ErrorMessage = "The field name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
