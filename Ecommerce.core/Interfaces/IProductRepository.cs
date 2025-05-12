using Ecommerce.core.DTO;
using Ecommerce.core.Entities.Product;

namespace Ecommerce.core.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<bool> AddAsync(AddProductDto productDto);
        Task<bool> UpdateAsync(UpdateProductDto productDto);
        Task DeleteAsync(Product product);
    }
}
