using ExchangeTrafic.Data.DbModels;
using ExchangeTrafic.Models.CurrencyModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExchangeTrafic.Services
{
    public class CurrencyService : ICurrencyService
    {
        private RatesContext _context;
        public CurrencyService(RatesContext context)
        {
            _context = context;
        }

        public async Task<ResponseRates> GetRatesAndSetIntoTransactionLogs(int id)
        {
            try
            {
                var query = await _context.Options.Where(opt => opt.Id == id).FirstOrDefaultAsync();
                if (query == null)
                {
                    return null;
                }
                string json = query.Headers;
                JObject jsonObject = JObject.Parse(json);
                string headerKey = jsonObject["key"].ToString(),
                    headerValue = jsonObject["value"].ToString(),
                    url = query.Url;

                //var headerDictionary = new Dictionary<string, string>();
                //headerDictionary.Add(headerKey, headerValue);
                //headerDictionary.Add("url", query.Url.ToString());
                //var header = headerDictionary.FirstOrDefault(x => x.Key == "apikey");
                //var url = headerDictionary.FirstOrDefault(x => x.Key == "url").Value;

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add(headerKey, headerValue);
                var result = await client.GetAsync(url);
                if (!result.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    var modelResultJson = await result.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<ResponseRates>(modelResultJson);
                    TransactionLog transactionLog = new TransactionLog()
                    {
                        CreatedDate = DateTime.Now,
                        RequestUrl = url,
                        ResponseeLog = modelResultJson
                    };

                    await _context.AddAsync(transactionLog);
                    await _context.SaveChangesAsync();

                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
