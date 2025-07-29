namespace AspNetCoreArchTemplate.Web.ViewModels.OrderItems
{
    public class OrderItemsIndexViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public int Quantity { get; set; }
        public string? Price { get; set; }
        public decimal Total { get; set; }
    }
}
