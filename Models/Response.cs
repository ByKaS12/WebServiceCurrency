using WebServiceCurrency.Classes;
using Newtonsoft.Json.Linq;
namespace WebServiceCurrency.Models
{
    public class Response
    {
        public DateTime Timestamp { get; set; }
        public List<Valute>? Сurrencies { get; set; }
        public Response()
        {

        }
        public Response(string address)
        {
            Сurrencies = new List<Valute>();
            GetRequest request = new(address);
            request.Run();
            var response = request.Response;
            if (response != null)
            {
                var Json = JObject.Parse(response);
                var time = Json["Timestamp"];
                Timestamp = Convert.ToDateTime(time);
                var valutes = Json["Valute"];
                int i = 0;
                foreach (var valute in valutes)
                {

                    var obj = valute.ToList().First();
                    Сurrencies.Add(new Valute()
                    {
                        Id = (string)obj["ID"],
                        NumCode = (string)obj["NumCode"],
                        CharCode = (string)obj["CharCode"],
                        Nominal = (int)obj["Nominal"],
                        Name = (string)obj["Name"],
                        Value = (double)obj["Value"],
                        PreviousValue = (double)obj["Previous"],

                    });
                    i++;
                }
            }
        }
        public Response? GetValute(string id) => new Response() { Timestamp = this.Timestamp, Сurrencies = new List<Valute>() { this.Сurrencies.FirstOrDefault(u=> u.Id == id)} }; 


    }
}
