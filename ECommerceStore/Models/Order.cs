using System.ComponentModel.DataAnnotations;

namespace ECommerceStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;
        public string? City { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        
    }
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,   
        Cancelled
    }
}
