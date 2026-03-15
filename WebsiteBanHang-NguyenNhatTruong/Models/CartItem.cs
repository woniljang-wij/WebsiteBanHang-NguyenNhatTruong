namespace WebsiteBanHang_NguyenNhatTruong.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public Product? Product { get; set; }
    }
}