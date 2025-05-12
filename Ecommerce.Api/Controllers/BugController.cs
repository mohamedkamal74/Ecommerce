using AutoMapper;
using Ecommerce.core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    public class BugController : BaseController
    {
        public BugController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("notFound")]
        public async Task<IActionResult> GetNotFound()
        {
            var category = await _unitOfWork.CategoryRepository.GetById(22);
            if (category is null) return NotFound();
            return Ok(category);
        }

        [HttpGet("serverError")]
        public async Task<IActionResult> GetServerError()
        {
            var category = await _unitOfWork.CategoryRepository.GetById(22);
            category.Name = "test"; //error
            return Ok(category);
        }

        [HttpGet("badRequest/{id}")]
        public async Task<IActionResult> GetBadRequest(int id)
        {
            return Ok();
        }


        [HttpGet("badRequest")]
        public async Task<IActionResult> GetBadRequest()
        {
            return BadRequest();
        }
    }
}
