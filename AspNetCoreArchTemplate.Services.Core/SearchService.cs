namespace AspNetCoreArchTemplate.Services.Core
{
    using AspNetCoreArchTemplate.Data.Models;
    using AspNetCoreArchTemplate.Data.Repository.Interfaces;
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Search;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class SearchService : ISearchService
    {
        private readonly IProductRepository productRepository;
        private readonly ICustomOrderRepository customOrderRepository;
        public SearchService(IProductRepository productRepository, ICustomOrderRepository customOrderRepository)
        {
            this.productRepository = productRepository;
            this.customOrderRepository = customOrderRepository;
        }
        public async Task<SearchResultsViewModel> SearchAsync(string query)
        {
            IEnumerable<Product> products = await this.productRepository
                .GetAllAttached()
                .AsNoTracking()
                .Where(p => p.Name.Contains(query)
                || p.Description.Contains(query))
                .ToListAsync();

            IEnumerable<CustomOrder> customOrders = await this.customOrderRepository
                .GetAllAttached()
                .Where(o => o.UserName.Contains(query)
                || o.Details.Contains(query))
                .ToListAsync();

            return new SearchResultsViewModel
            {
                Query = query,
                Products = products,
                CustomOrders = customOrders
            };
        }
    }
}
