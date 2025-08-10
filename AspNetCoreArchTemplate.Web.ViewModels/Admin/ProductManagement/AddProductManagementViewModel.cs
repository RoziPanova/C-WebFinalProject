namespace AspNetCoreArchTemplate.Web.ViewModels.Admin.ProductManagement
{
    using System.ComponentModel.DataAnnotations;

    public class AddProductManagementViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Range(1.00, double.MaxValue, ErrorMessage = "Pricing is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Specifying product type is required")]
        public string ProductType { get; set; } = null!;

        public string? EventType { get; set; }

        public string? ImageUrl { get; set; }

        public string? CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel>? Categories { get; set; }
                = new List<CategoryDropDownViewModel>();
    }
}
