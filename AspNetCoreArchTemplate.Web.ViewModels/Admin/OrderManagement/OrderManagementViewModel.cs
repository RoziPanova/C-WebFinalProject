namespace AspNetCoreArchTemplate.Web.ViewModels.Admin.OrderManagement
{
    public class OrderManagementViewModel
    {
        public IEnumerable<OrderViewModel>? Orders { get; set; }
        public IEnumerable<CustomOrderViewModel>? CustomOrders { get; set; }
    }
}
