namespace AspNetCoreArchTemplate.Web.ViewModels.Products
{
    public class ProductIndexViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public string ProductType { get; set; } = null!;

    }
}
