namespace AspNetCoreArchTemplate.Web.Controllers
{
    using AspNetCoreArchTemplate.Services.Core;
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class BouquetController : BaseController
    {
        private readonly IProductService productService;

        public BouquetController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            IEnumerable<ProductIndexViewModel> allBouquets = await this.productService
                .GetAllProductsAsync();


            var pageSize = 9;
            return View(await PaginatedList<ProductIndexViewModel>
                .CreatePaginationAsync(allBouquets, pageNumber ?? 1, pageSize));
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string? id)
        {
            try
            {
                ProductDetailsViewModel bouquet = await this.productService
                                .GetProductDetailsByIdAsync(id);
                if (bouquet == null)
                {
                    return View("/Home/Index");
                }

                return this.View(bouquet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("/Home/Index");
            }

        }
    }
}
