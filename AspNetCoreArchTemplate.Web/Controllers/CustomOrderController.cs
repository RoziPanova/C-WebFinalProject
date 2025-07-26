namespace AspNetCoreArchTemplate.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CustomOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
