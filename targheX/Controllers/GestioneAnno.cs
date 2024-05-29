using Microsoft.AspNetCore.Mvc;
using System.Linq;
using targheX.Data;
using targheX.Models;

namespace targheX.Controllers
{
    public class YearController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YearController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CloseYear(int year)
        {
            bool success = CloseYearInternal(year);
            if (success)
            {
                TempData["SuccessMessage"] = $"L'anno {year} è stato chiuso con successo.";
                return RedirectToAction("Index", "Items", new { year = DateTime.Now.Year }); // Reindirizza all'anno corrente);
            }
            else
            {
                TempData["ErrorMessage"] = "Errore nella chiusura dell'anno.";
                return RedirectToAction("Index", "Items");
            }
        }

        private bool CloseYearInternal(int year)
        {
            try
            {
                var itemsToClose = _context.Items.Where(i => i.Year == year && !i.IsClosed).ToList();
                foreach (var item in itemsToClose)
                {
                    item.IsClosed = true;
                }
                _context.SaveChanges();

                // Dopo aver chiuso l'anno corrente, crea la nuova tabella per l'anno successivo
                CreateNewYearTable(year + 1);

                return true;
            }
            catch
            {
                // Gestisci l'errore
                return false;
            }
        }

        private void CreateNewYearTable(int newYear)
        {
            var newItems = _context.Items.Where(i => i.Year == newYear - 1).Select(i => new Item
            {
                Year = newYear,
                DataIns = DateTime.Now,
                Name = i.Name,
                Giacenza = 0,
                GennaioCarico = 0,
                GennaioScarico = 0,
                FebbraioCarico = 0,
                FebbraioScarico = 0,
                MarzoCarico = 0,
                MarzoScarico = 0,
                AprileCarico = 0,
                AprileScarico = 0,
                MaggioCarico = 0,
                MaggioScarico = 0,
                GiugnoCarico = 0,
                GiugnoScarico = 0,
                LuglioCarico = 0,
                LuglioScarico = 0,
                AgostoCarico = 0,
                AgostoScarico = 0,
                SettembreCarico = 0,
                SettembreScarico = 0,
                OttobreCarico = 0,
                OttobreScarico = 0,
                NovembreCarico = 0,
                NovembreScarico = 0,
                DicembreCarico = 0,
                DicembreScarico = 0,
                Rimanenza = 0,
                TotaleCarico = 0,
                TotaleScarico = 0,
                Totale = 0,
                NuovoValore = 0,
                IsClosed = false
            }).ToList();

            _context.Items.AddRange(newItems);
            _context.SaveChanges();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
