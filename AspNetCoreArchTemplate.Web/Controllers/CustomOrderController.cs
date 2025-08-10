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
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CustomOrderFormInputViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }
            try
            {
                await this.customOrderService.AddCustomOrderAsync(inputModel);
                //TODO: Add a page for viewing customr orders
                return this.RedirectToAction(nameof(Create));
            }
            catch (Exception)
            {
                return this.RedirectToAction(nameof(Create));

            }
        }
    }
}
