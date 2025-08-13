namespace AspNetCoreArchTemplate.Web.ViewModels.CustomOrder
{
    public class CustomOrderDetailsViewModel
    {
        //public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateOnly RequestedDate { get; set; }
        public string Details { get; set; } = null!;
    }
}
