using AutoMapper;
using Ecommerce.Api.Helper;
using Ecommerce.core.DTO;
using Ecommerce.core.Entities.Product;
using Ecommerce.core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await _unitOfWork.CategoryRepository.GetAllAsync();
                if (data is null)
                    return BadRequest(new ResponeApi(400));
                return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetById(id);
                if (category is null)
                    return BadRequest(new ResponeApi(400,$"No Category Found with id= {id}"));
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDto);       
                await _unitOfWork.CategoryRepository.AddAsync(category);
                return Ok(new ResponeApi( 200,"Item has been Added" ));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> Update(UpdateCategoryDto categoryDto)
        {
            try
            {
                var category =_mapper.Map<Category>(categoryDto);
                await _unitOfWork.CategoryRepository.UpdateAsync(category);
                return Ok(new ResponeApi(200, "Item has been Updated"));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetById(id);
                if (category is null)
                    return NotFound();             
                await _unitOfWork.CategoryRepository.DeleteAsync(category.Id);
                return Ok(new ResponeApi(200, "Item has been Deleted"));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
