using Microsoft.AspNetCore.Mvc;
using WebServiceCurrency.Models;
using Newtonsoft.Json;

namespace WebServiceCurrency.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        string url = "https://www.cbr-xml-daily.ru/daily_json.js";



        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ILogger<CurrencyController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("currencies")]
        public string? GetCurrencies() => JsonConvert.SerializeObject(new Response(url),Formatting.Indented);

        [HttpGet("currency/{id}")]
        public string? GetCurrency(string id)=> JsonConvert.SerializeObject(new Response(url).GetValute(id), Formatting.Indented);

    }
}