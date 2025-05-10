using AutoMapper;
using Ecommerce.core.DTO;
using Ecommerce.core.Entities.Product;
using Ecommerce.core.Interfaces;
using Ecommerce.core.Services;
using Ecommerce.Infrastructure.Data;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagementService  _imageManagementService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
        }

        public async Task<bool> AddAsync(AddProductDto productDto)
        {
            if (productDto == null) return false;
            var product=_mapper.Map<Product>(productDto);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
           var imagesPath= await _imageManagementService.AddImageasync(productDto.Photos, productDto.Name);
            var photos = imagesPath.Select(x => new Photo
            {
              ImageName=x,
              ProductId=product.Id
            }).ToList();
            await _context.Photos.AddRangeAsync(photos);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
