using Microsoft.Extensions.Logging;
using System;

namespace targheX.Services
{
    public class YearServiceProxy : IYearService
    {
        private readonly IYearService _yearService;
        private readonly ILogger<YearServiceProxy> _logger;

        public YearServiceProxy(IYearService yearService, ILogger<YearServiceProxy> logger)
        {
            _yearService = yearService;
            _logger = logger;
        }

        public bool CloseYearInternal(int year)
        {
            // Log operation
            _logger.LogInformation($"Inizio chiusura anno: {year}");

            var result = _yearService.CloseYearInternal(year);

            if (result)
            {
                _logger.LogInformation($"Anno {year} chiuso con successo.");
            }
            else
            {
                _logger.LogError($"Chiusura dell'anno {year} fallita.");
            }

            return result;
        }

        public void CreateNewYearTable(int newYear)
        {
            // Log operation
            _logger.LogInformation($"Creo la nuova tabella per l'anno: {newYear}");

            _yearService.CreateNewYearTable(newYear);

            _logger.LogInformation($"Nuova tabella per l'anno {newYear} creata con successo.");
        }
    }
}
