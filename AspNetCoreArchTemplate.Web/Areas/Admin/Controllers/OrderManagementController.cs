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
                IEnumerable<OrderViewModel> orders = await orderManagementService
                    .GetAllOrdersAsync();
                IEnumerable<CustomOrderViewModel> customOrders = await orderManagementService
                    .GetAllCustomOrdersAsync();

                OrderManagementViewModel model = new OrderManagementViewModel
                {
                    Orders = orders,
                    CustomOrders = customOrders
                };

                return this.View(model);
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

                return this.RedirectToAction(nameof(Index), "OrderManagement");
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

                return this.RedirectToAction(nameof(Index), "OrderManagement");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return this.RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> OrderDetails(string id)
        {
            OrderDetailsViewModel? orderDetails = await orderManagementService
                .GetOrderDetailsAsync(id);

            if (orderDetails == null)
                return NotFound();

            return this.View(orderDetails);
        }

        [HttpGet]
        public async Task<IActionResult> CustomOrderDetails(string id)
        {
            CustomOrderViewModel? customOrder = await orderManagementService
                .GetCustomOrderDetailsAsync(id);

            if (customOrder == null)
                return NotFound();

            return this.View(customOrder);
        }

    }
}