using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using targheX.Data;
using targheX.Models;

namespace targheX.Controllers
{
    [Authorize(Roles = "Agenzia, Admin")]
    public class ItemsAgenzia: Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsAgenzia(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Items
        [HttpGet]
        public async Task<IActionResult> Inserimento()
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

        [HttpGet("InserimentoParziale/{anno?}")]
        public async Task<IActionResult> Inserimento(int? anno)
        {
            // Se non viene selezionato un anno, usa l'anno corrente
            int annoFiltrato = anno ?? DateTime.Now.Year;

            // Filtra gli oggetti Item per l'anno specificato
            var items = await _context.Items
                .Where(x => x.DataIns.Year == annoFiltrato)
                .ToListAsync();

            // Calcola i totali per ogni oggetto Item
            foreach (var item in items)
            {
                item.TotaleCarico = CalcolaTotaleCarico(item);
                item.TotaleScarico = CalcolaTotaleScarico(item);
                item.Totale = CalcolaTotale(item);
                item.Rimanenza = Rimanenza(item);
            }

            // Passa l'anno filtrato alla vista
            ViewBag.AnnoSelezionato = annoFiltrato;

            return View(items);
        }

        [HttpGet]
        public IActionResult InserimentoParziale(int? anno)
        {
            // Passa l'anno selezionato alla vista
            int annoFiltrato = anno ?? DateTime.Now.Year;

            ViewBag.AnnoSelezionato = annoFiltrato;

            // Crea una lista vuota di oggetti Item con solo l'anno impostato
            var itemsVuoti = _context.Items
                .Select(item => new Item
                {
                    ID = item.ID,
                    Name = item.Name,
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
                    DataIns = DateTime.Now
                })
                .ToList();

            return View(itemsVuoti);
        }

        [HttpPost]
        public IActionResult EsportaExcel(int anno, List<Item> items)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Dati Inseriti");
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalvaInserimentoParziale(int anno, List<Item> items)
        {
            foreach (var itemInput in items)
            {
                // Recupera l'oggetto esistente nel database per l'anno selezionato
                var itemEsistente = await _context.Items
                    .FirstOrDefaultAsync(x => x.ID == itemInput.ID && x.DataIns.Year == anno);

                if (itemEsistente == null)
                {
                    return NotFound($"L'oggetto con ID {itemInput.ID} non è stato trovato.");
                }

                // Somma i nuovi valori ai valori esistenti
                itemEsistente.Giacenza += itemInput.Giacenza;
                itemEsistente.GennaioCarico += itemInput.GennaioCarico;
                itemEsistente.GennaioScarico += itemInput.GennaioScarico;
                itemEsistente.FebbraioCarico += itemInput.FebbraioCarico;
                itemEsistente.FebbraioScarico += itemInput.FebbraioScarico;
                itemEsistente.MarzoCarico += itemInput.MarzoCarico;
                itemEsistente.MarzoScarico += itemInput.MarzoScarico;
                itemEsistente.AprileCarico += itemInput.AprileCarico;
                itemEsistente.AprileScarico += itemInput.AprileScarico;
                itemEsistente.MaggioCarico += itemInput.MaggioCarico;
                itemEsistente.MaggioScarico += itemInput.MaggioScarico;
                itemEsistente.GiugnoCarico += itemInput.GiugnoCarico;
                itemEsistente.GiugnoScarico += itemInput.GiugnoScarico;
                itemEsistente.LuglioCarico += itemInput.LuglioCarico;
                itemEsistente.LuglioScarico += itemInput.LuglioScarico;
                itemEsistente.AgostoCarico += itemInput.AgostoCarico;
                itemEsistente.AgostoScarico += itemInput.AgostoScarico;
                itemEsistente.SettembreCarico += itemInput.SettembreCarico;
                itemEsistente.SettembreScarico += itemInput.SettembreScarico;
                itemEsistente.OttobreCarico += itemInput.OttobreCarico;
                itemEsistente.OttobreScarico += itemInput.OttobreScarico;
                itemEsistente.NovembreCarico += itemInput.NovembreCarico;
                itemEsistente.NovembreScarico += itemInput.NovembreScarico;
                itemEsistente.DicembreCarico += itemInput.DicembreCarico;
                itemEsistente.DicembreScarico += itemInput.DicembreScarico;

                // Aggiorna i calcoli
                itemEsistente.TotaleCarico = CalcolaTotaleCarico(itemEsistente);
                itemEsistente.TotaleScarico = CalcolaTotaleScarico(itemEsistente);
                itemEsistente.Rimanenza = Rimanenza(itemEsistente);
                itemEsistente.Totale = CalcolaTotale(itemEsistente);

                _context.Update(itemEsistente);
            }

            // Salva le modifiche nel database
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(InserimentoParziale), new { anno = anno });
        }

        // GET: Items/Aggiungi
        public async Task<IActionResult> Add(int? id)
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

        // POST: Items/Aggiungi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int id, int anno, [Bind("ID,Name,Giacenza,GennaioCarico,GennaioScarico,FebbraioCarico,FebbraioScarico,MarzoCarico,MarzoScarico,AprileCarico,AprileScarico,MaggioCarico,MaggioScarico,GiugnoCarico,GiugnoScarico,LuglioCarico,LuglioScarico,AgostoCarico,AgostoScarico,SettembreCarico,SettembreScarico,OttobreCarico,OttobreScarico,NovembreCarico,NovembreScarico,DicembreCarico,DicembreScarico,DataIns,Year")] Item itemInput)
        {
            if (id != itemInput.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Recupera l'oggetto esistente per l'anno selezionato
                    var itemEsistente = await _context.Items
                        .FirstOrDefaultAsync(x => x.ID == id && x.DataIns.Year == anno);

                    if (itemEsistente == null)
                    {
                        return NotFound();
                    }

                    // Somma i nuovi valori ai valori esistenti
                    itemEsistente.GennaioCarico += itemInput.GennaioCarico;
                    itemEsistente.GennaioScarico += itemInput.GennaioScarico;
                    // Ripeti per tutti i mesi...

                    // Aggiorna i calcoli
                    itemEsistente.TotaleCarico = CalcolaTotaleCarico(itemEsistente);
                    itemEsistente.TotaleScarico = CalcolaTotaleScarico(itemEsistente);
                    itemEsistente.Rimanenza = Rimanenza(itemEsistente);
                    itemEsistente.Totale = CalcolaTotale(itemEsistente);

                    // Salva le modifiche
                    _context.Update(itemEsistente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(itemInput.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Inserimento), new { anno = anno });
            }

            return View(itemInput);
        }



        // Metodo privato per verificare l'esistenza di un oggetto Item nel database
        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ID == id);
        }

    }
}
