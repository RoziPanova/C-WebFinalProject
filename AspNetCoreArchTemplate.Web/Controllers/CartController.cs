namespace AspNetCoreArchTemplate.Web.Controllers
{
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Cart;
    using Microsoft.AspNetCore.Mvc;

    public class CartController : BaseController
    {
        private readonly ICartItemsService cartItemsService;
        public CartController(ICartItemsService cartItemsService)
        {
            this.cartItemsService = cartItemsService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                string? userId = this.GetUserId();
                if (userId == null)
                {
                    return this.Forbid();
                }

                IEnumerable<CartIndexViewModel> userCart = await this.cartItemsService
                    .GetAllCartItemsAysnc(userId);
                return View(userCart);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(string? productId)
        {
            try
            {
                string? userId = this.GetUserId();
                if (userId == null)
                {
                    return this.Forbid();
                }

                var result = await this.cartItemsService
                    .AddProductToCartAsync(productId, userId);
                if (result)
                {
                    TempData["SuccessMessage"] = "Product added to your cart!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Could not add product to cart.";
                }
                string returnUrl = Request.Headers["Referer"].ToString();
                return Redirect(returnUrl);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred.";
                Console.WriteLine(e.Message);

                return this.RedirectToAction(nameof(Index), "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Remove(string? productId)
        {
            try
            {
                string? userId = this.GetUserId();

                if (userId == null)
                {
                    return this.Forbid();
                }

                bool result = await cartItemsService
                    .RemoveProductFromCartAsync(productId, userId);


                return this.RedirectToAction(nameof(Index), "Cart");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            try
            {
                string? userId = this.GetUserId();

                if (userId == null)
                {
                    return this.Forbid();
                }

                bool result = await cartItemsService
                    .CheckoutAsync(userId);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this.RedirectToAction(nameof(Index), "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> IncreaseQuantity(string productId)
        {
            try
            {
                string? userId = this.GetUserId();
                if (userId == null)
                {
                    return Forbid();
                }
                await cartItemsService.IncreaseQuantityAsync(productId, userId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction(nameof(Index), "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> DecreaseQuantity(string productId)
        {
            try
            {
                string? userId = this.GetUserId();
                if (userId == null)
                {
                    return Forbid();
                }
                await cartItemsService.DecreaseQuantityAsync(productId, userId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return RedirectToAction(nameof(Index), "Home");
            }
        }
    }
}
