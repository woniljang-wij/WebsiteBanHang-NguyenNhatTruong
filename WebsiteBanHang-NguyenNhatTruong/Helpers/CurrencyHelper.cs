using System;

namespace WebsiteBanHang_NguyenNhatTruong.Helpers
{
    public static class CurrencyHelper
    {
        public static string ToVnd(decimal price)
        {
            return price.ToString("#,##0").Replace(",", ".") + "đ";
        }
    }
}