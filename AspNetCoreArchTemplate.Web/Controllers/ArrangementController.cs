namespace AspNetCoreArchTemplate.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Products;


    public class ArrangementController : BaseController
    {
        private readonly IArrangementService arrangementService;

        public ArrangementController(IArrangementService arrangementService)
        {
            this.arrangementService = arrangementService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductIndexViewModel> allArrangements = await this.arrangementService
                .GetAllProductsAsync();

            return View(allArrangements);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string? id)
        {
            try
            {
                ProductDetailsViewModel arrangement = await this.arrangementService
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
