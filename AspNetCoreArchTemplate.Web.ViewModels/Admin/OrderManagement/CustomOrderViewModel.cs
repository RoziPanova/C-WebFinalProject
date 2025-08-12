namespace AspNetCoreArchTemplate.Web.ViewModels.Admin.OrderManagement
{
    public class CustomOrderViewModel
    {
        public string Id { get; set; } = null!;
        public string CustomerName { get; set; } = null!;

        public string CustomerPhoneNumber { get; set; } = null!;
        public string CustomerAddress { get; set; } = null!;
        public DateOnly CustomOrderNeededBy { get; set; }
        public string CustomOrderDetails { get; set; } = null!;
    }
}