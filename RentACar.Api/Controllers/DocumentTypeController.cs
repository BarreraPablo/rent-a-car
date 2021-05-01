using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Core.DTOs.DocumentTypeDTOs;
using RentACar.Core.Entities;
using RentACar.Core.Exceptions;
using RentACar.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeService documentTypeService;
        private readonly IMapper mapper;

        public DocumentTypeController(IDocumentTypeService documentTypeService, IMapper mapper)
        {
            this.documentTypeService = documentTypeService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<DocumentType> docTypes = documentTypeService.GetAll();

            IEnumerable<DocumentTypeReadDto> docTypeDtos = mapper.Map<IEnumerable<DocumentTypeReadDto>>(docTypes);

            return Ok(docTypeDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Add(DocumentTypeCreateDto documentTypeDto)
        {
            DocumentType docType = mapper.Map<DocumentType>(documentTypeDto);

            await documentTypeService.Create(docType);

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(DocumentTypeReadDto docTypeDto)
        {
            try
            {
                DocumentType docType = mapper.Map<DocumentType>(docTypeDto);

                await documentTypeService.Update(docType);

                return NoContent();

            }
            catch (NullEntityException)
            {
                return NotFound();
            }
        }
    }
}
