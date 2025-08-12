namespace AspNetCoreArchTemplate.Web.ViewModels.Admin.OrderManagement
{
    public class OrderViewModel
    {
        public string Id { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public int TotalItemsCount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}