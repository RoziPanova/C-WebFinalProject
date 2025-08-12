namespace AspNetCoreArchTemplate.Web.Areas.Admin.Controllers
{
    using AspNetCoreArchTemplate.Services.Core.Admin.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Admin.OrderManagement;
    using Microsoft.AspNetCore.Mvc;

    [AutoValidateAntiforgeryToken]
    public class OrderManagementController : BaseAdminController
    {
        private readonly IOrderManagementService orderManagementService;

        public OrderManagementController(IOrderManagementService orderManagementService)
        {
            this.orderManagementService = orderManagementService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var orders = await orderManagementService
                    .GetAllOrdersAsync();
                var customOrders = await orderManagementService
                    .GetAllCustomOrdersAsync();

                var model = new OrderManagementViewModel
                {
                    Orders = orders,
                    CustomOrders = customOrders
                };

                return View(model);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return this.RedirectToAction(nameof(Index), "Home");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            try
            {
                bool success = await orderManagementService
                    .DeleteOrderAsync(id);

                if (!success)
                    return NotFound();

                return this.RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return this.RedirectToAction(nameof(Index), "Home");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCustomOrder(string id)
        {
            try
            {
                bool success = await orderManagementService
                    .DeleteCustomOrderAsync(id);

                if (!success)
                    return NotFound();

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