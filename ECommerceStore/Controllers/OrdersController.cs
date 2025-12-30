using ECommerceStore.Data;
using ECommerceStore.Helpers;
using ECommerceStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerceStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string CartSessionKey = "ShoppingCart";
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            return View(orders);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var order = await _context.Orders
                .Include(o=>o.OrderItems)
                .FirstOrDefaultAsync(o=>o.Id == id);
            if(order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        public IActionResult Checkout()
        {
            var cart = GetCart();

            if (cart == null || !cart.Items.Any())
            {
                TempData["Error"] = "Your cart is empty! Please add products first.";
                return RedirectToAction("Index", "Products");
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {
            var cart = GetCart();
            if(cart == null || !cart.Items.Any())
            {
                TempData["Error"] = "Cart is Empty!";
                return RedirectToAction("Index", "Products");
            }
            if (ModelState.IsValid)
            {
                // Create order
                order.OrderDate = DateTime.Now;
                order.Status = OrderStatus.Pending;
                order.TotalAmount = cart.TotalPrice;

                // Add order items
                foreach (var cartItem in cart.Items)
                {
                    order.OrderItems.Add(new OrderItem
                    {
                        ProductId = cartItem.ProductId,
                        ProductName = cartItem.ProductName,
                        Price = cartItem.Price,
                        Quantity = cartItem.Quantity
                    });

                    var product = await _context.Products.FindAsync(cartItem.ProductId);
                    if (product != null)
                    {
                        product.Stock -= cartItem.Quantity;
                    }
                }
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                HttpContext.Session.Remove(CartSessionKey);

                TempData["Success"] = "Your order has been placed successfully!";
                return RedirectToAction(nameof(Confirmation), new { id = order.Id });
            }

            return View(order);
        }
        public async Task<IActionResult> Confirmation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        // Helper Methods
        private Cart GetCart()
        {
            return HttpContext.Session.GetObjectFromJson<Cart>(CartSessionKey);
        }
    }
}
