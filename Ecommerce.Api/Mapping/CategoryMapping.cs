using AutoMapper;
using Ecommerce.core.DTO;
using Ecommerce.core.Entities.Product;

namespace Ecommerce.Api.Mapping
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryDto,Category>().ReverseMap();
            CreateMap<UpdateCategoryDto,Category>().ReverseMap();
        }
    }
}
