using System.ComponentModel.DataAnnotations;

namespace ECommerceStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required" )]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage ="Description is Required")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Price is Required")]
        [Range(0.01, double.MaxValue, ErrorMessage ="Price Should Be Larger Than 0")]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        [Required(ErrorMessage = "Stock is Required")]
        [Range(0,int.MaxValue, ErrorMessage = "Stock Must be 0 or Bigger")]
        public int Stock { get; set; }
        [Required(ErrorMessage ="Category is Required")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
