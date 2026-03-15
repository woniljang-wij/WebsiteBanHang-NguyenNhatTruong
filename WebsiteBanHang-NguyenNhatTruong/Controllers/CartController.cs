using Microsoft.AspNetCore.Mvc;
using WebsiteBanHang_NguyenNhatTruong.Models;
using WebsiteBanHang_NguyenNhatTruong.Repositories;
using System.Text.Json;

namespace WebsiteBanHang_NguyenNhatTruong.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CartController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        const string CARTKEY = "cart";

        // Lấy giỏ hàng từ Session
        List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session.GetString(CARTKEY);

            if (session != null)
            {
                return JsonSerializer.Deserialize<List<CartItem>>(session) ?? new List<CartItem>();
            }

            return new List<CartItem>();
        }

        // Lưu giỏ hàng vào Session
        void SaveCartSession(List<CartItem> ls)
        {
            HttpContext.Session.SetString(CARTKEY, JsonSerializer.Serialize(ls));
        }

        // 🔴 Đếm tổng số sản phẩm trong giỏ
        public int GetCartCount()
        {
            var cart = GetCartItems();
            return cart.Sum(p => p.Quantity);
        }

        // Thêm sản phẩm vào giỏ
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            var cart = GetCartItems();

            var item = cart.FirstOrDefault(p => p.ProductId == productId);

            if (item == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity += quantity;
            }

            SaveCartSession(cart);

            return RedirectToAction("Index");
        }

        // Hiển thị giỏ hàng
        public async Task<IActionResult> Index()
        {
            var cart = GetCartItems();

            foreach (var item in cart)
            {
                item.Product = await _productRepository.GetByIdAsync(item.ProductId);
            }

            return View(cart);
        }

        // Xóa sản phẩm
        public IActionResult Remove(int id)
        {
            var cart = GetCartItems();

            cart.RemoveAll(p => p.ProductId == id);

            SaveCartSession(cart);

            return RedirectToAction("Index");
        }

        // Tăng số lượng
        public IActionResult Increase(int id)
        {
            var cart = GetCartItems();

            var item = cart.FirstOrDefault(p => p.ProductId == id);

            if (item != null)
            {
                item.Quantity++;
            }

            SaveCartSession(cart);

            return RedirectToAction("Index");
        }

        // Giảm số lượng
        public IActionResult Decrease(int id)
        {
            var cart = GetCartItems();

            var item = cart.FirstOrDefault(p => p.ProductId == id);

            if (item != null)
            {
                item.Quantity--;

                if (item.Quantity <= 0)
                {
                    cart.Remove(item);
                }
            }

            SaveCartSession(cart);

            return RedirectToAction("Index");
        }
    }
}