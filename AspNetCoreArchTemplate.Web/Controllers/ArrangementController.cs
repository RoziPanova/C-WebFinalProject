namespace AspNetCoreArchTemplate.Web.Controllers
{
    using AspNetCoreArchTemplate.Services.Core;
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;


    public class ArrangementController : BaseController
    {
        private readonly IProductService productService;

        public ArrangementController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            IEnumerable<ProductIndexViewModel> allArrangements = await this.productService
                .GetAllProductsAsync();

            var pageSize = 9;
            return View(await PaginatedList<ProductIndexViewModel>
                .CreatePaginationAsync(allArrangements, pageNumber ?? 1, pageSize));
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string? id)
        {
            try
            {
                ProductDetailsViewModel arrangement = await this.productService
                                .GetProductDetailsByIdAsync(id);
                if (arrangement == null)
                {
                    return View();
                }

                return this.View(arrangement);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }

        }
    }
}
