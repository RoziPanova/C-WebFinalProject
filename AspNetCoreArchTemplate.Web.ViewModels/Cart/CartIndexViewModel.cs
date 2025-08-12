namespace AspNetCoreArchTemplate.Web.ViewModels.Cart
{
    public class CartIndexViewModel
    {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string ProductType { get; set; } = null!;
        public string ProductImageUrl { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        //public decimal CartTotal { get; set; }
    }
}
