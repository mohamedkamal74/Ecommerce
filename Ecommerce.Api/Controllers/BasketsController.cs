using AutoMapper;
using Ecommerce.Api.Helper;
using Ecommerce.core.Entities;
using Ecommerce.core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    public class BasketsController : BaseController
    {
        public BasketsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("getBasketItem/{id}")]
        public async Task<IActionResult> get(string id)
        {
            var result = await _unitOfWork.CustomerBasketRepository.GetCustomerBasketAsync(id);
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(CustomerBasket customerBasket)
        {
            var result = await _unitOfWork.CustomerBasketRepository.UpdateBasketAsync(customerBasket);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _unitOfWork.CustomerBasketRepository.DeleteBasketAsync(id);
            return result ? Ok(new ResponeApi(200, "Item Deleted Succesfully"))
                : BadRequest(new ResponeApi(400, "Item not Deleted "));
        }
    }
}
