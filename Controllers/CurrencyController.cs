using Microsoft.AspNetCore.Mvc;
using WebServiceCurrency.Models;
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
        public Response? GetCurrencies() => new Response(url);

        [HttpGet("currency/{id}")]
        public Response? GetCurrency(string id)=> new Response(url).GetValute(id);

    }
}