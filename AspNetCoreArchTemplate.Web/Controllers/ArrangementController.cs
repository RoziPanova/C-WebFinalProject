﻿namespace AspNetCoreArchTemplate.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ArrangementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
