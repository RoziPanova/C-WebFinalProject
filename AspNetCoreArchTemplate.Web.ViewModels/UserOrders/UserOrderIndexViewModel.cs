namespace AspNetCoreArchTemplate.Web.ViewModels.UserOrders
{
    public class UserOrderIndexViewModel
    {
        public string OrderItemId { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public string ItemType { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int Quantity { get; set; }
        public string? Price { get; set; }
        public decimal Total { get; set; }
    }
}
