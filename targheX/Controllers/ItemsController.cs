using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using targheX.Data;
using targheX.Models;

namespace targheX.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            // Carica gli oggetti Item dal database
            var items = await _context.Items.ToListAsync();

            // Calcola i valori TotaleCarico, TotaleScarico, Rimanenza e Totale per ogni oggetto Item
            foreach (var item in items)
            {
                item.TotaleCarico = CalcolaTotaleCarico(item);
                item.TotaleScarico = CalcolaTotaleScarico(item);
                item.Totale = CalcolaTotale(item);
                item.Rimanenza = Rimanenza(item);
            }

            return View(items);
        }

        // Metodo per calcolare il totale di carico per un oggetto Item
        private int CalcolaTotaleCarico(Item item)
        {
            // Calcola il totale di carico utilizzando le proprietà mensili e la giacenza iniziale
            return item.GennaioCarico + item.FebbraioCarico + item.MarzoCarico + item.AprileCarico + item.MaggioCarico +
                   item.GiugnoCarico + item.LuglioCarico + item.AgostoCarico + item.SettembreCarico + item.OttobreCarico +
                   item.NovembreCarico + item.DicembreCarico;
        }

        // Metodo per calcolare il totale di scarico per un oggetto Item
        private int CalcolaTotaleScarico(Item item)
        {
            // Calcola il totale di scarico utilizzando le proprietà mensili
            return item.GennaioScarico + item.FebbraioScarico + item.MarzoScarico + item.AprileScarico + item.MaggioScarico +
                   item.GiugnoScarico + item.LuglioScarico + item.AgostoScarico + item.SettembreScarico + item.OttobreScarico +
                   item.NovembreScarico + item.DicembreScarico;
        }

        // Metodo per calcolare la rimanenza per un oggetto Item
        private int Rimanenza(Item item)
        {
            // Calcola la rimaneza sommando la giacenza iniziale al carico totale e sottraendo lo scarico totale
            return CalcolaTotaleCarico(item) + item.Giacenza - CalcolaTotaleScarico(item);
        }

        // Metodo per calcolare il totale per un oggetto Item
        private int CalcolaTotale(Item item)
        {
            // Calcola il totale sommando scarico totale e rimanenza
            return CalcolaTotaleScarico(item) + Rimanenza(item);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Giacenza,GennaioCarico,GennaioScarico,FebbraioCarico,FebbraioScarico,MarzoCarico,MarzoScarico,AprileCarico,AprileScarico,MaggioCarico,MaggioScarico,GiugnoCarico,GiugnoScarico,LuglioCarico,LuglioScarico,AgostoCarico,AgostoScarico,SettembreCarico,SettembreScarico,OttobreCarico,OttobreScarico,NovembreCarico,NovembreScarico,DicembreCarico,DicembreScarico")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Giacenza,GennaioCarico,GennaioScarico,FebbraioCarico,FebbraioScarico,MarzoCarico,MarzoScarico,AprileCarico,AprileScarico,MaggioCarico,MaggioScarico,GiugnoCarico,GiugnoScarico,LuglioCarico,LuglioScarico,AgostoCarico,AgostoScarico,SettembreCarico,SettembreScarico,OttobreCarico,OttobreScarico,NovembreCarico,NovembreScarico,DicembreCarico,DicembreScarico")] Item item)
        {
            if (id != item.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Metodo privato per verificare l'esistenza di un oggetto Item nel database
        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ID == id);
        }
    }
}

