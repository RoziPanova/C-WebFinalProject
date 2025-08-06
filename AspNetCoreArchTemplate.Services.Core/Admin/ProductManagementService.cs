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
        public async Task<ProductManagementFormInputModel?> GetByIdAsync(string? productId)
        {
            bool result = Guid.TryParse(productId, out var id);
            if (result)
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
                    .AsNoTracking()
                    .IgnoreQueryFilters()
                    .SingleOrDefaultAsync(p => p.Id.ToString() == model.Id);
            if (product == null)
                return false;

            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.ImageUrl = model.ImageUrl;
            bool result = Guid.TryParse(model.CategoryId, out Guid categoryId);
            if (result)
            {
                product.CategoryId = categoryId;
            }
            else
            {
                product.CategoryId = null;
            }
            product.IsDeleted = model.IsAvailable;

            await productRepository.SaveChangesAsync();

            return true;
        }
        public async Task<bool> HardDeleteAsync(string? productId)
        {
            bool res = Guid.TryParse(productId, out Guid prodId);
            if (res)
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
            bool res = Guid.TryParse(model.CategoryId, out Guid categoryId);
            Product? product = null;
            if (res)
            {
                product = new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    ProductType = model.ProductType,
                    EventType = model.EventType ?? null,
                    ImageUrl = model.ImageUrl ?? $"{NoImageUrl}",
                    CategoryId = categoryId,
                    IsDeleted = false
                };

            }
            else
            {
                product = new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    ProductType = model.ProductType,
                    EventType = model.EventType ?? null,
                    ImageUrl = model.ImageUrl ?? $"{NoImageUrl}",
                    CategoryId = null,
                    IsDeleted = false
                };
            }

            await productRepository.AddAsync(product);
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
