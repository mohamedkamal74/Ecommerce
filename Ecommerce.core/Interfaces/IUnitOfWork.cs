namespace Ecommerce.core.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get;  }
        public IProductRepository ProductRepository { get;  }
        public IPhotoRepository PhotoRepository { get;  }
        public ICustomerBasketRepository CustomerBasketRepository { get;  }
    }
}
