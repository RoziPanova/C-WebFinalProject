namespace AspNetCoreArchTemplate.Web.ViewModels.Products
{
    public class ProductDetailsViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? EventType { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
