namespace AspNetCoreArchTemplate.Web.Controllers
{
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.CustomOrder;
    using Microsoft.AspNetCore.Mvc;

    [AutoValidateAntiforgeryToken]
    public class CustomOrderController : BaseController
    {
        private readonly ICustomOrderService customOrderService;
        public CustomOrderController(ICustomOrderService customOrderService)
        {
            this.customOrderService = customOrderService;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomOrderFormInputViewModel inputModel)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return this.View(inputModel);
                }
                await this.customOrderService
                    .AddCustomOrderAsync(inputModel);
                //TODO: Add a page for viewing customr orders
                return this.RedirectToAction(nameof(Create));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return this.RedirectToAction("Index", "Home");

            }
        }
    }
}
