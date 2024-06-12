using System;

namespace targheX.Services
{
    public class YearServiceProxy : IYearService
    {
        private readonly IYearService _yearService;

        public YearServiceProxy(IYearService yearService)
        {
            _yearService = yearService;
        }

        public bool CloseYearInternal(int year)
        {
            // Log operazioni per chiusura
            Console.WriteLine($"Chiusura anno: {year}");

            var result = _yearService.CloseYearInternal(year);

            if (result)
            {
                Console.WriteLine($"Anno {year} chiuso con successo.");
            }
            else
            {
                Console.WriteLine($"Chiusura dell'anno {year} fallita.");
            }

            return result;
        }

        public void CreateNewYearTable(int newYear)
        {
            // Log operazioni per nuova tabella
            Console.WriteLine($"Creo la nuova tabella per l'nno: {newYear}");

            _yearService.CreateNewYearTable(newYear);

            Console.WriteLine($"Nuova tabella per l'anno {newYear} creata con successo.");
        }
    }
}

