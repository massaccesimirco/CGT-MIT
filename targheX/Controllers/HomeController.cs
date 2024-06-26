﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using targheX.Models;

namespace targheX.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //return View();
            // Effettua il reindirizzamento alla pagina di login di Identity
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        public IActionResult Success()
        {
            return View("Success");
        }

        public IActionResult Info()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}