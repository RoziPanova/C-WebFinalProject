namespace AspNetCoreArchTemplate.Web.ViewModels.Admin.OrderManagement
{
    public class OrderDetailsViewModel
    {
        public string Id { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public List<OrderItemViewModel> Items { get; set; } = new();
    }

    public class OrderItemViewModel
    {
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
