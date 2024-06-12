using Microsoft.Extensions.Logging;
using targheX.Models;

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
            if (_yearService.IsYearClosed(year))
            {
                _logger.LogWarning($"L'anno {year} è già chiuso. Non puoi chiuderlo di nuovo.");
                return false;
            }

            _logger.LogInformation($"Sto chiudendo l'anno: {year}");

            var result = _yearService.CloseYearInternal(year);

            if (result)
            {
                _logger.LogInformation($"L'anno {year} è stato chiuso con successo.");
            }
            else
            {
                _logger.LogError($"Non è stato possibile chiudere l'anno {year}.");
            }

            return result;
        }

        public void CreateNewYearTable(int newYear)
        {
            if (_yearService.IsYearClosed(newYear - 1))
            {
                _logger.LogInformation($"Creo la nuova tabella relativa all'anno: {newYear}");
                _yearService.CreateNewYearTable(newYear);
                _logger.LogInformation($"Nuova tabella per l'anno {newYear} creata con successo.");
            }
            else
            {
                _logger.LogWarning($"Non posso creare la tabella per l'anno {newYear} perchè l'anno {newYear - 1} non è ancora stato chiuso.");
            }
        }

        public bool IsYearClosed(int year)
        {
            return _yearService.IsYearClosed(year);
        }
    }
}
