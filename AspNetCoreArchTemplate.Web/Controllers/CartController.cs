namespace AspNetCoreArchTemplate.Web.Controllers
{
    using AspNetCoreArchTemplate.Services.Core.Interfaces;
    using AspNetCoreArchTemplate.Web.ViewModels.Products;
    using AspNetCoreArchTemplate.Web.ViewModels.Cart;
    using Microsoft.AspNetCore.Mvc;

    public class CartController : BaseController
    {

        public async Task<IActionResult> Index()
        {
            return this.View();
        }
    }
}
