using Microsoft.AspNetCore.Mvc;
using System;
using targheX.Services;

namespace targheX.Controllers
{
    public class YearController : Controller
    {
        private readonly IYearService _yearService;

        public YearController(IYearService yearService)
        {
            _yearService = yearService;
        }

        [HttpPost]
        public IActionResult CloseYear(int year)
        {
            bool success = _yearService.CloseYearInternal(year);
            if (success)
            {
                TempData["SuccessMessage"] = $"L'anno {year} è stato chiuso con successo.";
                return RedirectToAction("Index", "Items", new { year = DateTime.Now.Year });
            }
            else
            {
                TempData["ErrorMessage"] = "Errore nella chiusura dell'anno o l'anno è già chiuso.";
                return RedirectToAction("Index", "Items");
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
