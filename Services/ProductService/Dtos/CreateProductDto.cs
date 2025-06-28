using System.ComponentModel.DataAnnotations;

namespace ProductService.Dtos
{
    public class CreateProductDto
    {
        [Required, StringLength(200)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public decimal Cost { get; set; }
    }
}
