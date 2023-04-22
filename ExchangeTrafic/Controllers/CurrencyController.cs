using ExchangeTrafic.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeTrafic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }


        [HttpGet("GetRates")]
        public async Task<IActionResult> GetRatesAndSetIntoTransactionLogs(int id)
        {
            try
            {
                var response = await _currencyService.GetRatesAndSetIntoTransactionLogs(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
