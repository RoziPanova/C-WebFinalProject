namespace AspNetCoreArchTemplate.Web.Controllers
{
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Bouquet;
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
            IEnumerable<BouquetIndexViewModel> allBouquets = await this.bouquetService
                .GetAllBouquetsAsync();

            return View(allBouquets);
        }
    }
}
