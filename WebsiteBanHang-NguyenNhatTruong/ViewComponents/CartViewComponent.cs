using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebsiteBanHang_NguyenNhatTruong.Models;

namespace WebsiteBanHang_NguyenNhatTruong.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        const string CARTKEY = "cart";

        public IViewComponentResult Invoke()
        {
            var session = HttpContext.Session.GetString(CARTKEY);

            int count = 0;

            if (session != null)
            {
                var cart = JsonSerializer.Deserialize<List<CartItem>>(session);
                count = cart.Sum(p => p.Quantity);
            }

            return View("Default", count);
        }
    }
}