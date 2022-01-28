using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.Application.DTOs.Products
{
    public record CreateProductDto
    {
        [Required(ErrorMessage = "{0} cannot be null.", AllowEmptyStrings = false)]
        [StringLength(254, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        [DisplayName("ProductName")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} cannot be null.", AllowEmptyStrings = false)]
        [StringLength(254, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 10)]
        [DisplayName("ProductDescription")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "{0} cannot be null.")]
        [DataType(DataType.Currency)]
        [DisplayName("ProductPrice")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "{0} cannot be null.")]
        [DisplayName("ProductStock")]
        public int Stock { get; set; }

        [DisplayName("ProductImage")]
        public string Image { get; set; }

        [Required(ErrorMessage = "{0} cannot be null.")]
        [DisplayName("Id")]
        public int CategoryId { get; set; }
    }
}