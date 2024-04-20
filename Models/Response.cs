using WebServiceCurrency.Classes;
using Newtonsoft.Json.Linq;
using path_watcher.Models;
namespace WebServiceCurrency.Models
{
    public class Response
    {
        private readonly ApplicationContext? context;
        public Valute? valute { get; set; }
        public Response()
        {
            
        }
        public Response(ApplicationContext Context)
        {
            context = Context;
        }
        public Valute AddParam(string address, string country)
        {
            valute = new Valute();
            GetRequest request = new(address);
            request.Run();
            var response = request.Response;
            if (response != null)
            {
                //var Col = response.Split('\n').First(x => x.Contains("Country")).Split("|");
                var row = response.Split('\n').First(x => x.Contains($"{country}")).Split("|");
                valute.Country = row[0];
                valute.Currency = row[1];
                valute.Amount = row[2];
                valute.Code = row[3];
                valute.Rate = Convert.ToDouble(row[4].Replace('.',','));
                valute.DateTime = DateTime.Now;
            }
            return valute;
        }
        //public Response? GetValute(string id) => new Response() { Timestamp = this.Timestamp, Сurrencies = new List<Valute>() { this.Сurrencies.FirstOrDefault(u=> u.Id == id)} }; 


    }
}
