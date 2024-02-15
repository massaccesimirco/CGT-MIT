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
    public class ItemShow : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemShow(ApplicationDbContext context)
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

        // Metodo privato per verificare l'esistenza di un oggetto Item nel database
        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ID == id);
        }

    }
}
