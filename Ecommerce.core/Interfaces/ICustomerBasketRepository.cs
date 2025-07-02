using Ecommerce.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.core.Interfaces
{
    public interface ICustomerBasketRepository
    {
        Task<CustomerBasket> GetCustomerBasketAsync(string id);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket);
        Task<bool> DeleteBasketAsync(string id);
    }
}
