namespace AspNetCoreArchTemplate.Web.ViewModels.Admin.ProductManagement
{
    using System.ComponentModel.DataAnnotations;

    public class AddProductManagementViewModel
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string ProductType { get; set; } = null!;

        public string? EventType { get; set; }

        public string? ImageUrl { get; set; }

        public string? CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; } = null!;
    }
}
