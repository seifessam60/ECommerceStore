using Microsoft.AspNetCore.Mvc;
using ECommerceStore.Data;
using ECommerceStore.Models;
using ECommerceStore.Helpers;

namespace ECommerceStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string CartSessionKey = "ShoppingCart";

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var product = _context.Products.Find(productId);

            if (product == null)
            {
                return NotFound();
            }
            var cart = GetCart();
            cart.AddItem(product, quantity);
            SaveCart(cart);

            TempData["Success"] = $"Added {product.Name} to cart successfully!";
            return RedirectToAction(nameof(Index),"Products");
        }
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            cart.UpdateQuantity(productId, quantity);
            SaveCart(cart);

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult RemoveItem(int productId)
        {
            var cart = GetCart();
            cart.RemoveItem(productId);
            SaveCart(cart);

            TempData["Success"] = "Product removed from cart!";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Clear()
        {
            HttpContext.Session.Remove(CartSessionKey);
            TempData["Success"] = "Cart cleared successfully!";
            return RedirectToAction(nameof(Index));
        }
        public Cart GetCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>(CartSessionKey);
            return cart ?? new Cart();
        }
        public void SaveCart(Cart cart)
        {
            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
        }
    }
}
