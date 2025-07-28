namespace AspNetCoreArchTemplate.Web.Controllers
{
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class BouquetController : BaseController
    {
        private readonly IBouquetService bouquetService;

        public BouquetController(IBouquetService bouquetService)
        {
            this.bouquetService = bouquetService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductIndexViewModel> allBouquets = await this.bouquetService
                .GetAllProductsAsync();

            return View(allBouquets);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string? id)
        {
            try
            {
                ProductDetailsViewModel bouquet = await this.bouquetService
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
