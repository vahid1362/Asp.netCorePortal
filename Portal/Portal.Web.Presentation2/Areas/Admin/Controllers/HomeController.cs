﻿using Microsoft.AspNetCore.Mvc;

namespace Portal.Web.Presentation2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}