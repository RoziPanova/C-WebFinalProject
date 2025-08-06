namespace AspNetCoreArchTemplate.Web.ViewModels.Admin.Products
{
    public class ProductManagementIndexViewModel
    {
        public string Id { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string? CategoryName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
