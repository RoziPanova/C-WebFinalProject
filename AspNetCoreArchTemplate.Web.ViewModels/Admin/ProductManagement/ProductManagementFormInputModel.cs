namespace AspNetCoreArchTemplate.Web.ViewModels.Admin.ProductManagement
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;

    public class ProductManagementFormInputModel
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public decimal Price { get; set; }

        public string? Description { get; set; }
        [Required]
        public string ImageUrl { get; set; } = null!;

        public string? CategoryId { get; set; }
        public IEnumerable<CategoryDropDownViewModel>? Categories { get; set; }
            = new List<CategoryDropDownViewModel>();
        [Required]
        public bool IsAvailable { get; set; }
    }
}
