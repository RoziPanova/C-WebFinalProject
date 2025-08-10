using AspNetCoreArchTemplate.Web.ViewModels.Admin.ProductManagement;
using AspNetCoreArchTemplate.Web.ViewModels.Admin.Products;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreArchTemplate.Services.Core.Admin.Interfaces
{
    public interface IProductManagementService
    {
        public Task<IEnumerable<ProductManagementIndexViewModel>> GetAllProductsAsync();
        public Task<ProductManagementFormInputModel?> GetByIdAsync(string? productId);
        public Task<IEnumerable<CategoryDropDownViewModel>> GetAllCategoriesAsync();
        public Task<bool> UpdateAsync(ProductManagementFormInputModel model);
        public Task<bool> HardDeleteAsync(string? productId);
        public Task<bool> AddProductAsync(AddProductManagementViewModel model);
        public Task<bool> DeleteAsync(string? productId);

    }
}
