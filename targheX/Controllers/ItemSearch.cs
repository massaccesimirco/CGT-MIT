using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using targheX.Data;
using targheX.Models;
using Microsoft.AspNetCore.Authorization;

namespace targheX.Controllers
{
    [Authorize(Roles = "Ufficio, Admin")]
    public class ItemSearchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemSearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItemSearch
        public async Task<IActionResult> Index(string searchItems, DateTime? startDate, DateTime? endDate, string selectedMonth)
        {
            ViewData["CurrentFilter"] = searchItems;
            ViewData["CurrentStartDate"] = startDate?.ToString("yyyy-MM-dd");
            ViewData["CurrentEndDate"] = endDate?.ToString("yyyy-MM-dd");
            ViewData["SelectedMonth"] = selectedMonth;

            var months = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Gennaio" },
                new SelectListItem { Value = "2", Text = "Febbraio" },
                new SelectListItem { Value = "3", Text = "Marzo" },
                new SelectListItem { Value = "4", Text = "Aprile" },
                new SelectListItem { Value = "5", Text = "Maggio" },
                new SelectListItem { Value = "6", Text = "Giugno" },
                new SelectListItem { Value = "7", Text = "Luglio" },
                new SelectListItem { Value = "8", Text = "Agosto" },
                new SelectListItem { Value = "9", Text = "Settembre" },
                new SelectListItem { Value = "10", Text = "Ottobre" },
                new SelectListItem { Value = "11", Text = "Novembre" },
                new SelectListItem { Value = "12", Text = "Dicembre" }
            };

            ViewBag.Months = months;

            var items = from i in _context.Items select i;

            if (!string.IsNullOrEmpty(searchItems))
            {
                items = items.Where(i => i.Name.Contains(searchItems));
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                items = items.Where(i => i.DataIns >= startDate && i.DataIns <= endDate);
            }

            if (!string.IsNullOrEmpty(selectedMonth) && selectedMonth != "all")
            {
                int month = int.Parse(selectedMonth);
                items = items.Where(i => i.DataIns.Month == month);
            }

            var searchItemsResults = await items.ToListAsync();

            foreach (var item in searchItemsResults)
            {
                item.TotaleCarico = CalcolaTotaleCarico(item);
                item.TotaleScarico = CalcolaTotaleScarico(item);
                item.Totale = CalcolaTotale(item);
                item.Rimanenza = Rimanenza(item);
            }

            return View(searchItemsResults);
        }

        private int CalcolaTotaleCarico(Item item)
        {
            return item.GennaioCarico + item.FebbraioCarico + item.MarzoCarico + item.AprileCarico + item.MaggioCarico +
                   item.GiugnoCarico + item.LuglioCarico + item.AgostoCarico + item.SettembreCarico + item.OttobreCarico +
                   item.NovembreCarico + item.DicembreCarico;
        }

        private int CalcolaTotaleScarico(Item item)
        {
            return item.GennaioScarico + item.FebbraioScarico + item.MarzoScarico + item.AprileScarico + item.MaggioScarico +
                   item.GiugnoScarico + item.LuglioScarico + item.AgostoScarico + item.SettembreScarico + item.OttobreScarico +
                   item.NovembreScarico + item.DicembreScarico;
        }

        private int Rimanenza(Item item)
        {
            return CalcolaTotaleCarico(item) + item.Giacenza - CalcolaTotaleScarico(item);
        }

        private int CalcolaTotale(Item item)
        {
            return CalcolaTotaleScarico(item) + Rimanenza(item);
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ID == id);
        }

        public IActionResult GeneratePdf()
        {
            var items = _context.Items.ToList();

            return new ViewAsPdf("PdfView", items)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
            };
        }
    }
}
