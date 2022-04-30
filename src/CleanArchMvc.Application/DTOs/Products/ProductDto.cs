using CleanArchMvc.Application.DTOs.Categories;

namespace CleanArchMvc.Application.DTOs.Products
{
    public record ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }
    }
}