﻿using AutoMapper;
using Ecommerce.core.DTO;
using Ecommerce.core.Entities.Product;

namespace Ecommerce.Api.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.CategoryName, op => op.MapFrom(x => x.Category.Name))
                .ReverseMap();

            CreateMap<Photo, PhotoDto>().ReverseMap();

            CreateMap<AddProductDto, Product>()
                .ForMember(x=>x.Photos,op=>op.Ignore())
                .ReverseMap();

            CreateMap<UpdateProductDto, Product>()
               .ForMember(x => x.Photos, op => op.Ignore())
               .ReverseMap();

        }
    }
}
