namespace AspNetCoreArchTemplate.Services.Core
{
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Products;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static AspNetCoreArchTemplate.GCommon.ApplicationConstants;

    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<IEnumerable<ProductIndexViewModel>> GetAllProductsAsync()
        {

            IEnumerable<ProductIndexViewModel> allProducts = await this.productRepository
                .GetAllAttached()
                .AsNoTracking()
                .Select(p => new ProductIndexViewModel()
                {
                    Id = p.Id.ToString(),
                    Name = p.Name,
                    ImageUrl = p.ImageUrl ?? $"{NoImageUrl}",
                    ProductType = p.ProductType,
                })
                .ToListAsync();

            return allProducts;
        }

        public async Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(string? id)
        {
            ProductDetailsViewModel? productsDetails = null;

            bool isIdValidGuid = Guid.TryParse(id, out Guid productId);
            if (isIdValidGuid)
            {
                productsDetails = await this.productRepository
                    .GetAllAttached()
                    .AsNoTracking()
                    .Where(p => p.Id == productId)
                    .Select(p => new ProductDetailsViewModel()
                    {
                        Id = p.Id.ToString(),
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description,
                        EventType = p.EventType,
                        ImageUrl = p.ImageUrl ?? $"{NoImageUrl}",
                        ProductType = p.ProductType,
                    })
                    .SingleOrDefaultAsync();
            }

            return productsDetails!;
        }
    }
}

