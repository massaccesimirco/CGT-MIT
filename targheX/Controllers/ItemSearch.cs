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
        public async Task<IActionResult> Index(string searchItems, DateTime? startDate, DateTime? endDate, string selectedMonth, int? year)
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

            // Ottieni l'anno corrente se non è specificato alcun anno
            if (!year.HasValue)
            {
                year = DateTime.Now.Year;
            }

            ViewBag.SelectedYear = year;

            var items = from i in _context.Items where i.Year == year select i;

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

        [HttpGet]
        public IActionResult EsportaExcel(int anno)
        {
            var items = _context.Items
                .Where(x => x.DataIns.Year == anno)
                .ToList();

            using (var workbook = new ClosedXML.Excel.XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add($"Dati Anno {anno}");
                int currentRow = 1;

                // Aggiunge l'intestazione
                worksheet.Cell(currentRow, 1).Value = "Nome";
                worksheet.Cell(currentRow, 2).Value = "Giacenza";
                worksheet.Cell(currentRow, 3).Value = "Gennaio Carico";
                worksheet.Cell(currentRow, 4).Value = "Gennaio Scarico";
                worksheet.Cell(currentRow, 5).Value = "Febbraio Carico";
                worksheet.Cell(currentRow, 6).Value = "Febbraio Scarico";
                worksheet.Cell(currentRow, 7).Value = "Marzo Carico";
                worksheet.Cell(currentRow, 8).Value = "Marzo Scarico";
                worksheet.Cell(currentRow, 9).Value = "Aprile Carico";
                worksheet.Cell(currentRow, 10).Value = "Aprile Scarico";
                worksheet.Cell(currentRow, 11).Value = "Maggio Carico";
                worksheet.Cell(currentRow, 12).Value = "Maggio Scarico";
                worksheet.Cell(currentRow, 13).Value = "Giugno Carico";
                worksheet.Cell(currentRow, 14).Value = "Giugno Scarico";
                worksheet.Cell(currentRow, 15).Value = "Luglio Carico";
                worksheet.Cell(currentRow, 16).Value = "Luglio Scarico";
                worksheet.Cell(currentRow, 17).Value = "Agosto Carico";
                worksheet.Cell(currentRow, 18).Value = "Agosto Scarico";
                worksheet.Cell(currentRow, 19).Value = "Settembre Carico";
                worksheet.Cell(currentRow, 20).Value = "Settembre Scarico";
                worksheet.Cell(currentRow, 21).Value = "Ottobre Carico";
                worksheet.Cell(currentRow, 22).Value = "Ottobre Scarico";
                worksheet.Cell(currentRow, 23).Value = "Novembre Carico";
                worksheet.Cell(currentRow, 24).Value = "Novembre Scarico";
                worksheet.Cell(currentRow, 25).Value = "Dicembre Carico";
                worksheet.Cell(currentRow, 26).Value = "Dicembre Scarico";
                worksheet.Cell(currentRow, 27).Value = "Totale Carico";
                worksheet.Cell(currentRow, 28).Value = "Totale Scarico";
                worksheet.Cell(currentRow, 29).Value = "Totale";
                worksheet.Cell(currentRow, 30).Value = "Rimanenza";

                // Aggiunge i dati
                foreach (var item in items)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.Name;
                    worksheet.Cell(currentRow, 2).Value = item.Giacenza;
                    worksheet.Cell(currentRow, 3).Value = item.GennaioCarico;
                    worksheet.Cell(currentRow, 4).Value = item.GennaioScarico;
                    worksheet.Cell(currentRow, 5).Value = item.FebbraioCarico;
                    worksheet.Cell(currentRow, 6).Value = item.FebbraioScarico;
                    worksheet.Cell(currentRow, 7).Value = item.MarzoCarico;
                    worksheet.Cell(currentRow, 8).Value = item.MarzoScarico;
                    worksheet.Cell(currentRow, 9).Value = item.AprileCarico;
                    worksheet.Cell(currentRow, 10).Value = item.AprileScarico;
                    worksheet.Cell(currentRow, 11).Value = item.MaggioCarico;
                    worksheet.Cell(currentRow, 12).Value = item.MaggioScarico;
                    worksheet.Cell(currentRow, 13).Value = item.GiugnoCarico;
                    worksheet.Cell(currentRow, 14).Value = item.GiugnoScarico;
                    worksheet.Cell(currentRow, 15).Value = item.LuglioCarico;
                    worksheet.Cell(currentRow, 16).Value = item.LuglioScarico;
                    worksheet.Cell(currentRow, 17).Value = item.AgostoCarico;
                    worksheet.Cell(currentRow, 18).Value = item.AgostoScarico;
                    worksheet.Cell(currentRow, 19).Value = item.SettembreCarico;
                    worksheet.Cell(currentRow, 20).Value = item.SettembreScarico;
                    worksheet.Cell(currentRow, 21).Value = item.OttobreCarico;
                    worksheet.Cell(currentRow, 22).Value = item.OttobreScarico;
                    worksheet.Cell(currentRow, 23).Value = item.NovembreCarico;
                    worksheet.Cell(currentRow, 24).Value = item.NovembreScarico;
                    worksheet.Cell(currentRow, 25).Value = item.DicembreCarico;
                    worksheet.Cell(currentRow, 26).Value = item.DicembreScarico;
                    worksheet.Cell(currentRow, 27).Value = item.TotaleCarico;
                    worksheet.Cell(currentRow, 28).Value = item.TotaleScarico;
                    worksheet.Cell(currentRow, 29).Value = item.Totale;
                    worksheet.Cell(currentRow, 30).Value = item.Rimanenza;
                }

                using (var stream = new System.IO.MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Dati_Anno_{anno}.xlsx");
                }
            }
        }
        public IActionResult GeneratePdf()
        {
            var items = _context.Items.ToList();

            return new ViewAsPdf("PdfView", items)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                CustomSwitches = "--print-media-type --margin-top 20 --margin-bottom 20"
            };
        }
    }
}
