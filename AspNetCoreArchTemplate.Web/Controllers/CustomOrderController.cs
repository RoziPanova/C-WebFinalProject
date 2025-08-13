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
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<CustomOrderListViewModel> customOrders =
                                await this.customOrderService
                                .GetUserCustomOrdersAsync(GetUserId()!);
                return this.View(customOrders);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return this.RedirectToAction(nameof(Index), "Home");
            }

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
                    .AddCustomOrderAsync(inputModel, GetUserId()!);
                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return this.RedirectToAction(nameof(Index), "Home");

            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var model = await customOrderService
                                .GetCustomOrderForEditAsync(id);

                if (model == null)
                    return NotFound();

                return View("Create", model); // reusing the Create form
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return this.RedirectToAction(nameof(Index), "Home");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, CustomOrderFormInputViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("Create", model);

                var success = await customOrderService
                    .UpdateCustomOrderAsync(id, model);

                if (!success)
                {
                    TempData["ErrorMessage"] = "Orders can only be edited at least 3 days before the needed-by date.";
                    return RedirectToAction(nameof(Index));
                }

                TempData["SuccessMessage"] = "Order updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return this.RedirectToAction(nameof(Index), "Home");
            }
        }

    }
}
