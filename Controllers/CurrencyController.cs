using Microsoft.AspNetCore.Mvc;
using WebServiceCurrency.Models;
using Newtonsoft.Json;
using WebServiceCurrency.Classes;


namespace WebServiceCurrency.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CurrencyController : ControllerBase
    {



        private readonly ApplicationContext context;
        public DateTime date = DateTime.Today.Date;
        public string url = string.Empty;
        public string country = "Japan";
        public string codes = "JPY|IDR|PHP";
        public string startDate = string.Empty;
        public string endDate = string.Empty;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ILogger<CurrencyController> logger)
        {
            _logger = logger;
             url = $"https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/daily.txt?date={date}";
            
    }
        
        [HttpGet("currencies")]
        public string? GetCurrencies(string country) => JsonConvert.SerializeObject(new Response().AddParam(url, country), Formatting.Indented);
        [HttpGet("Sync")]
        public void Sync() {

            var obj = new Response().AddParam(url, country);
            new CRUD().CreateOrChange(obj);

        }
        [HttpGet("SyncRange")]
        public void SyncRange(string year,string codes,string startDate,string endDate)
        {
            DateTime sdate = Convert.ToDateTime(startDate);
            DateTime edate = Convert.ToDateTime(endDate);
            var raz = edate.Year - sdate.Year;
            List<int> years = new List<int>();
            for (int i = 0; i < raz+1; i++)
            {
                years.Add(sdate.Year+i);
            }
            foreach (var item in years)
            {
                var obj = new Response().SyncRange($"https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/year.txt?year={item}", codes, startDate, endDate);
                new CRUD().CreateDateRange(obj, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate));
            }


        }
        [HttpGet("JsonAnswer")]
        public string? JsonAnswer(string codes, string startDate, string endDate) => JsonConvert.SerializeObject(new CRUD().Answers(codes, Convert.ToDateTime(startDate), Convert.ToDateTime(endDate)), Formatting.Indented);
    }
}