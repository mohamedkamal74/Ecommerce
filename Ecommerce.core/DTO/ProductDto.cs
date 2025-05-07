﻿namespace Ecommerce.core.DTO
{
    public record ProductDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public virtual List<PhotoDto> Photos { get; set; }
        public string CategoryName { get; set; }
    }

    public record PhotoDto
    {
        public string ImageName { get; set; }
        public int ProductId { get; set; }

    }
}
