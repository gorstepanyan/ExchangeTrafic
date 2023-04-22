using ExchangeTrafic.Models.CurrencyModels;

namespace ExchangeTrafic.Services
{
    public interface ICurrencyService
    {
        public Task<ResponseRates> GetRatesAndSetIntoTransactionLogs(int id);
    }
}
