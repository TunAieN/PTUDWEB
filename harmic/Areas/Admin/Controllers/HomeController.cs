﻿using Microsoft.AspNetCore.Mvc;

namespace harmic.Areas.admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
