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
    public class ItemSearch : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemSearch(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(string searchItems)
        {
            // Carica gli oggetti Item dal database
            IQueryable<Item> items = _context.Items;

            // Se è presente un termine di ricerca, filtra gli oggetti in base al termine
            if (!string.IsNullOrEmpty(searchItems))
            {
                items = items.Where(item => item.Name.Contains(searchItems));
            }

            // Esegui la query per ottenere i risultati
            var searchItemsResults = await items.ToListAsync();

            // Calcola i valori TotaleCarico, TotaleScarico, Rimanenza e Totale per ogni oggetto Item
            foreach (var item in searchItemsResults)
            {
                item.TotaleCarico = CalcolaTotaleCarico(item);
                item.TotaleScarico = CalcolaTotaleScarico(item);
                item.Totale = CalcolaTotale(item);
                item.Rimanenza = Rimanenza(item);
            }

            return View(searchItemsResults);
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
