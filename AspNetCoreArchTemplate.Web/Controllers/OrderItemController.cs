namespace AspNetCoreArchTemplate.Web.Controllers
{
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.OrderItems;
    using Microsoft.AspNetCore.Mvc;

    public class OrderItemController : BaseController
    {
        private readonly IOrderItemsService orderItemsService;
        public OrderItemController(IOrderItemsService orderItemsService)
        {
            this.orderItemsService = orderItemsService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<OrderItemsIndexViewModel> allOrderedItems = await
                this.orderItemsService
                .GetAllOrderedItemsAync();

            return View(allOrderedItems);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(string? id)
        {
            await this.orderItemsService.AddItemToCart(id);
            return View();
        }
    }
}
