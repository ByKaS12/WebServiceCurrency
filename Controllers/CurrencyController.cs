using Microsoft.AspNetCore.Mvc;
using WebServiceCurrency.Models;
using Newtonsoft.Json;


namespace WebServiceCurrency.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CurrencyController : ControllerBase
    {



        private readonly ApplicationContext contex;
        public DateTime date = DateTime.Today.Date;
        public string url = string.Empty;
        public string country = "Japan";
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ILogger<CurrencyController> logger, ApplicationContext Context)
        {
            _logger = logger;
            contex = Context;
             url = $"https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing/daily.txt?date={date}";
            
    }
        
        [HttpGet("currencies")]
        public string? GetCurrencies(string country) => JsonConvert.SerializeObject(new Response().AddParam(url, country), Formatting.Indented);
        [HttpGet("Sync")]
        public void Sync() {
            


        }
            

        //[HttpGet("currency/{date}")]
        //public string? GetCurrency(string date)=> JsonConvert.SerializeObject(new Response(url).GetValute(date), Formatting.Indented);

    }
}