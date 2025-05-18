using AutoMapper;
using Ecommerce.core.DTO;
using Ecommerce.core.Entities.Product;
using Ecommerce.core.Interfaces;
using Ecommerce.core.Services;
using Ecommerce.core.Sharing;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            _context = context;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(ProductParam productParam)
        {
            var query = _context.Products.Include(p => p.Category).Include(p => p.Photos).AsNoTracking();

            if (!string.IsNullOrEmpty(productParam.search))
                query = query.Where(p => p.Name.ToLower().Contains(productParam.search.ToLower())
                  ||p.Description.Contains(productParam.search.ToLower()));

            if (productParam.categoryId.HasValue)
                query = query.Where(p => p.CategoryId == productParam.categoryId);
            if (!string.IsNullOrEmpty(productParam.sort))
            {
                query = productParam.sort switch
                {
                    "PriceAce" => query.OrderBy(p => p.NewPrice),
                    "PriceDce" => query.OrderByDescending(p => p.NewPrice),
                    _ => query.OrderByDescending(p => p.Name),
                };
            }

            query = query.Skip((productParam.pageSize) * (productParam.pageNumber - 1)).Take(productParam.pageSize);

            var result = _mapper.Map<List<ProductDto>>(query);
            return result;
        }
        public async Task<bool> AddAsync(AddProductDto productDto)
        {
            if (productDto == null) return false;
            var product = _mapper.Map<Product>(productDto);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            var imagesPath = await _imageManagementService.AddImageasync(productDto.Photos, productDto.Name);
            var photos = imagesPath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.Id
            }).ToList();
            await _context.Photos.AddRangeAsync(photos);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateProductDto productDto)
        {
            if (productDto is null) return false;
            var ExistingProduct = await _context.Products.Include(x => x.Category).Include(x => x.Photos)
                            .FirstOrDefaultAsync(x => x.Id == productDto.Id);
            if (ExistingProduct is null) return false;
            _mapper.Map(productDto, ExistingProduct);

            var ExistingPhoto = await _context.Photos.Where(x => x.ProductId == productDto.Id).ToListAsync();
            foreach (var item in ExistingPhoto)
            {
                _imageManagementService.DeleteImageAsync(item.ImageName);
            }

            _context.Photos.RemoveRange(ExistingPhoto);
            var imagePath = await _imageManagementService.AddImageasync(productDto.Photos, productDto.Name);

            var photos = imagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = productDto.Id
            }).ToList();

            await _context.Photos.AddRangeAsync(photos);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(Product product)
        {
            if (product is null) return;
            if (product is not null && product.Photos is not null && product.Photos.Any())
            {
                foreach (var photo in product.Photos)
                {
                    _imageManagementService.DeleteImageAsync(photo.ImageName);
                }
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}