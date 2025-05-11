using AutoMapper;
using Ecommerce.Api.Helper;
using Ecommerce.core.DTO;
using Ecommerce.core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Ecommerce.Api.Controllers
{
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await _unitOfWork.ProductRepository
                                             .GetAllAsync(x=>x.Category,x=>x.Photos);
                var result = _mapper.Map<List<ProductDto>>(data);
                if (data is null)
                    return BadRequest(new ResponeApi(400));
                return Ok(result);
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
                var product = await _unitOfWork.ProductRepository.GetById(id, x => x.Category, x => x.Photos);
                if (product is null) return BadRequest(new ResponeApi(400, $"No product found with id= {id}"));
                var result=_mapper.Map<ProductDto>(product);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AddProductDto addProductDto)
        {
            try
            {
              await  _unitOfWork.ProductRepository.AddAsync(addProductDto);
                return Ok(new ResponeApi(200));
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponeApi(400, ex.Message));
            }
        }

        [HttpPut]

        public async Task<IActionResult> Update([FromForm] UpdateProductDto updateProductDto)
        {
            try
            {
                await _unitOfWork.ProductRepository.UpdateAsync(updateProductDto);
                return Ok(new ResponeApi(200));
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponeApi(400, ex.Message));
            }
        }
    }
}
