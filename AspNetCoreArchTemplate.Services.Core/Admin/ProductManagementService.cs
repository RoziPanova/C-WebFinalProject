using AspNetCoreArchTemplate.Data.Repository.Interfaces;
namespace AspNetCoreArchTemplate.Services.Core.Admin
{
    using AspNetCoreArchTemplate.Data.Models;
    using AspNetCoreArchTemplate.Services.Core.Admin.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Admin.ProductManagement;
    using AspNetCoreArchTemplate.Web.ViewModels.Admin.Products;
    using Microsoft.EntityFrameworkCore;

    using static AspNetCoreArchTemplate.GCommon.ApplicationConstants;

    public class ProductManagementService : IProductManagementService
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        public ProductManagementService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<ProductManagementIndexViewModel>> GetAllProductsAsync()
        {
            IEnumerable<ProductManagementIndexViewModel> products = await productRepository
                .GetAllAttached()
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Include(p => p.Category)
                .Select(p => new ProductManagementIndexViewModel
                {
                    Id = p.Id.ToString(),
                    ImageUrl = p.ImageUrl ?? $"{NoImageUrl}",
                    Name = p.Name,
                    Price = p.Price,
                    CategoryName = p.Category.Name,
                    IsDeleted = p.IsDeleted,
                })
                .ToListAsync();

            return products;
        }
        public async Task<ProductManagementFormInputModel?> GetProductByIdAsync(string? productId)
        {
            bool isIdValidGuid = Guid.TryParse(productId, out var id);
            if (isIdValidGuid)
            {
                var product = await productRepository
                    .GetAllAttached()
                    .AsNoTracking()
                    .IgnoreQueryFilters()
                    .SingleOrDefaultAsync(p => p.Id == id);

                if (product == null)
                    return null;

                return new ProductManagementFormInputModel
                {
                    Id = id.ToString(),
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    CategoryId = product.CategoryId.ToString(),
                    ProductType = product.ProductType,
                    EventType = product.EventType,
                    IsAvailable = product.IsDeleted
                };
            }
            else
            {
                return null;
            }
        }



        public async Task<bool> UpdateAsync(ProductManagementFormInputModel model)
        {

            if (model.Id == null)
                return false;

            var product = await productRepository
                .GetAllAttached()
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(p => p.Id.ToString() == model.Id.ToLower());

            if (product == null)
                return false;

            // Map the new values
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.ImageUrl = model.ImageUrl;
            product.ProductType = model.ProductType;
            product.EventType = model.EventType;
            product.CategoryId = string.IsNullOrEmpty(model.CategoryId)
                ? null
                : Guid.Parse(model.CategoryId);
            product.IsDeleted = model.IsAvailable;

            await productRepository.UpdateAsync(product);

            await productRepository.SaveChangesAsync();

            return true;
        }
        public async Task<bool> HardDeleteAsync(string? productId)
        {
            bool isIdValidGuid = Guid.TryParse(productId, out Guid prodId);
            if (isIdValidGuid)
            {
                var product = await this.productRepository
                                .SingleOrDefaultAsync(p => p.Id == prodId);

                if (product == null)
                {
                    return false;
                }

                await productRepository.DeleteAsync(product); // Hard delete
                await productRepository.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AddProductAsync(AddProductManagementViewModel model)
        {

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl ?? $"{NoImageUrl}",
                ProductType = model.ProductType,
                EventType = model.EventType,
                CategoryId = string.IsNullOrEmpty(model.CategoryId) ? null : Guid.Parse(model.CategoryId)
            };


            await productRepository.AddAsync(product);
            await productRepository.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(string? productId)
        {
            if (productId == null)
                return false;

            var product = await productRepository
                .GetAllAttached()
                .IgnoreQueryFilters()
                .SingleOrDefaultAsync(p => p.Id.ToString() == productId);

            if (product == null)
            {
                return false;
            }

            await productRepository.HardDeleteAsync(product);

            await productRepository.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<CategoryDropDownViewModel>> GetAllCategoriesAsync()
        {
            IEnumerable<CategoryDropDownViewModel> categories = await categoryRepository
                     .GetAllAttached()
                     .AsNoTracking()
                     .Select(c => new CategoryDropDownViewModel
                     {
                         Id = c.Id.ToString(),
                         Name = c.Name,
                     }).ToArrayAsync();
            return categories;
        }
    }
}
