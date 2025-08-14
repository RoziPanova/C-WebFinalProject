namespace AspNetCoreArchTemplate.Web.Controllers
{
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Products;
    using AspNetCoreArchTemplate.Web.ViewModels.Search;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : BaseController
    {
        private readonly ISearchService searchService;
        private readonly IProductService productService;

        public SearchController(ISearchService searchService, IProductService productService)
        {
            this.searchService = searchService;
            this.productService = productService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return this.View(new SearchResultsViewModel());
            }

            var results = await this.searchService
                .SearchAsync(query);
            return this.View(results);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string? id)
        {
            try
            {
                ProductDetailsViewModel product = await
                    this.productService
                    .GetProductDetailsByIdAsync(id);
                if (product == null)
                {
                    return this.RedirectToAction(nameof(Index), "Home");
                }

                return this.View("_ProductDetailsPartial", product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }

        }
    }
}
