namespace AspNetCoreArchTemplate.Web.ViewModels.CustomOrder
{
    public class CustomOrderListViewModel
    {
        public string Id { get; set; } = null!;
        public DateOnly RequestedDate { get; set; }
        public string Details { get; set; } = null!;
    }
}
