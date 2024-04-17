using WebServiceCurrency.Classes;
using Newtonsoft.Json.Linq;
namespace WebServiceCurrency.Models
{
    public class Response
    {
        public string Timestamp { get; set; } = DateTime.Now.Date.ToShortDateString();
        public Valute valute { get; set; }
        public Response()
        {

        }
        public Response(string address, string country)
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
            }
        }
        //public Response? GetValute(string id) => new Response() { Timestamp = this.Timestamp, Сurrencies = new List<Valute>() { this.Сurrencies.FirstOrDefault(u=> u.Id == id)} }; 


    }
}
