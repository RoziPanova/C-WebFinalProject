namespace AspNetCoreArchTemplate.Services.Core
{
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Products;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using static AspNetCoreArchTemplate.GCommon.ApplicationConstants;

    public class ArrangementService : IArrangementService
    {
        private readonly IArrangementRepository arrangementRepository;
        public ArrangementService(IArrangementRepository arrangementRepository)
        {
            this.arrangementRepository = arrangementRepository;
        }

        public async Task<IEnumerable<ProductIndexViewModel>> GetAllProductsAsync()
        {
            IEnumerable<ProductIndexViewModel> allProducts = await this.arrangementRepository
                .GetAllAttached()
                .AsNoTracking()
                .Select(b => new ProductIndexViewModel()
                {
                    Id = b.Id.ToString(),
                    Name = b.Name,
                    ImageUrl = b.ImageUrl,
                })
                .ToListAsync();
            foreach (ProductIndexViewModel product in allProducts)
            {
                if (String.IsNullOrEmpty(product.ImageUrl))
                {
                    product.ImageUrl = $"{NoImageUrl}";
                }
            }
            return allProducts;
        }

        public async Task<ProductDetailsViewModel> GetProductDetailsByIdAsync(string? id)
        {
            ProductDetailsViewModel? productsDetails = null;

            bool isIdValidGuid = Guid.TryParse(id, out Guid productId);
            if (isIdValidGuid)
            {
                productsDetails = await this.arrangementRepository
                    .GetAllAttached()
                    .AsNoTracking()
                    .Where(b => b.Id == productId)
                    .Select(b => new ProductDetailsViewModel()
                    {
                        Id = b.Id.ToString(),
                        Name = b.Name,
                        Price = b.Price,
                        Description = b.Description,
                        ImageUrl = b.ImageUrl ?? $"{NoImageUrl}",
                    })
                    .SingleOrDefaultAsync();
            }

            return productsDetails;
        }
    }
}
