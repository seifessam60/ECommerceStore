using System.ComponentModel.DataAnnotations;

namespace ECommerceStore.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name is Required")]
        [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;
        [StringLength(500,ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }
        public List<Product>? Products { get; set; }
    }
}
