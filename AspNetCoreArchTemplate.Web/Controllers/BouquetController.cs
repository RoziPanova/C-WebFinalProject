namespace AspNetCoreArchTemplate.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class BouquetController : BaseController
    {
        public BouquetController() { }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
    }
}
