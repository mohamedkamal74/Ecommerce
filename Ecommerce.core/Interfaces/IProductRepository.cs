using Ecommerce.core.DTO;
using Ecommerce.core.Entities.Product;
using Ecommerce.core.Sharing;

namespace Ecommerce.core.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<ReturnProductDto> GetAllAsync(ProductParam productParam);
        Task<bool> AddAsync(AddProductDto productDto);
        Task<bool> UpdateAsync(UpdateProductDto productDto);
        Task DeleteAsync(Product product);
    }
}
