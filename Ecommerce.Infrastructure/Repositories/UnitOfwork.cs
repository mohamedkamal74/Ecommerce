using Ecommerce.core.Interfaces;
using Ecommerce.Infrastructure.Data;

namespace Ecommerce.Infrastructure.Repositories
{
    public class UnitOfwork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IPhotoRepository PhotoRepository { get; }

        public UnitOfwork(AppDbContext context)
        {
            _context = context;
            CategoryRepository=new CategoryRepository(_context);
            ProductRepository=new ProductRepository(_context);
            PhotoRepository=new PhotoRepository(_context);
        }
    }
}
