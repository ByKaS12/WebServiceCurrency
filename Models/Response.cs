using WebServiceCurrency.Classes;
using Newtonsoft.Json.Linq;
using Castle.Core.Internal;
namespace WebServiceCurrency.Models
{
    public struct Curr
    {
        public int Amount;
        public string Code;
        public string Date;
    }
    public class Response
    {
        public Valute? valute { get; set; }
        public Response()
        {
            
        }

        public Valute AddParam(string address, string country)
        {
            valute = new Valute();
            GetRequest request = new(address);
            request.Run();
            var response = request.Response;
            if (response != null)
            {
                //var Col = response.Split('\n').First(x => x.Contains("Date")).Split("|");
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
        public List<Valute> SyncRange(string address, string codes,string dateStart,string dateEnd)
        {
            valute = new Valute();
            List<Valute> valutes = new List<Valute>();
            List<Curr> currs = new List<Curr>();
            GetRequest request = new(address);
            request.Run();
            var response = request.Response;
            if (response != null)
            {
                var obj = response.Split('\n');
                var col = obj[0].Split("|");
                foreach (var item in col.Skip(1))
                {
                    var test = item.Split(" ");
                    
                    currs.Add(new Curr() { Amount = Convert.ToInt32(test[0]), Code = test[1] });
                }
                var str = codes.Split("|");
                List<int> selectedItems = new List<int>();
                foreach (var item in str)
                {
                    var index = currs.FindIndex(x => x.Code == item);
                    if (index > 0)
                    { selectedItems.Add(index); }

                }
                for (int i = 1; i < obj.Count()-1; i++)
                {
                    var rows = obj[i].Split("|");
                    foreach(var item in selectedItems)
                    {
                        valutes.Add(new Valute()
                        {
                            Id = Guid.NewGuid(),
                            DateTime = Convert.ToDateTime(rows[0]),
                            Amount = Convert.ToString(currs[item].Amount),
                            Code = Convert.ToString(currs[item].Code),
                            Rate = Convert.ToDouble(rows[item].Replace('.', ','))

                        });
                    }
                }

                //var Col = response.Split('\n').First(x => x.Contains("Country")).Split("|");
                //var row = response.Split('\n').First(x => x.Contains($"{country}")).Split("|");
                //valute.Country = row[0];
                //valute.Currency = row[1];
                //valute.Amount = row[2];
                //valute.Code = row[3];
                //valute.Rate = Convert.ToDouble(row[4].Replace('.', ','));
                //valute.DateTime = DateTime.Now;
            }
            //var test222 = valutes.FindAll(x => x.DateTime > Convert.ToDateTime(dateStart) & x.DateTime < Convert.ToDateTime(dateEnd));
            return valutes.FindAll(x=> x.DateTime >= Convert.ToDateTime(dateStart) & x.DateTime <= Convert.ToDateTime(dateEnd));
        }
        //public Response? GetValute(string id) => new Response() { Timestamp = this.Timestamp, Сurrencies = new List<Valute>() { this.Сurrencies.FirstOrDefault(u=> u.Id == id)} }; 


    }
}
