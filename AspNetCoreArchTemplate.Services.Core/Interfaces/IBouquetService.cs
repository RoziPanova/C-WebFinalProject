namespace AspNetCoreArchTemplate.Services.Core.Interfaces
{
    using AspNetCoreArchTemplate.Web.ViewModels.Products;

    public interface IBouquetService
    {
        Task<IEnumerable<ProductIndexViewModel>> GetAllProductsAsync();
        Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(string? id);
    }
}
