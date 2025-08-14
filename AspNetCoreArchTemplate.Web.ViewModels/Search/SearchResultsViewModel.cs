namespace AspNetCoreArchTemplate.Web.ViewModels.Search
{
    using AspNetCoreArchTemplate.Data.Models;

    public class SearchResultsViewModel
    {
        public string Query { get; set; } = null!;
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public IEnumerable<CustomOrder> CustomOrders { get; set; } = new List<CustomOrder>();
    }

}
