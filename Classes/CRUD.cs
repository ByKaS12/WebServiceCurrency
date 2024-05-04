using Newtonsoft.Json.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using WebServiceCurrency.Models;

namespace WebServiceCurrency.Classes
{
    public class CRUD
    {
        private readonly ApplicationContext context = new ApplicationContext();
        public void CreateOrChange(Valute valute)
        {
            var entity = context.Valutes.FirstOrDefault(x => x.Country == valute.Country);
            if (entity == null)
            {
                _ = context.Valutes.Add(valute);
                _ = context.SaveChanges();
            }
            else
            {
                entity.Rate = valute.Rate;
                entity.DateTime = DateTime.Now;
                _ = context.Valutes.Update(entity);
                _ = context.SaveChanges();
            }
        }
        public void CreateDateRange(List<Valute> valutes,DateTime start, DateTime end)
        {
            var Codes = new List<string>();
            foreach (var valute in valutes)
            {
                if(Codes.Find(x=>x==valute.Code)==null)
                    Codes.Add(valute.Code);
                
            }
            var test = valutes;
            foreach (var code in Codes) {
                var entity = context.Valutes.ToList().FindAll(x => x.Code == code & (x.DateTime >= start & x.DateTime <= end));
                var test2 = test.FindAll(x => x.Code == code);
                var addDB = test2.Except(entity, new CurrComparer());
                if (addDB.Count()!=0)
                {
                    context.Valutes.AddRange(addDB);

                }
            }
            
            _ = context.SaveChanges();
        }
        public class CurrComparer : IEqualityComparer<Valute>
        {
            // Products are equal if their names and product numbers are equal.
            public bool Equals(Valute x, Valute y)
            {

                //Check whether the compared objects reference the same data.
                if (Object.ReferenceEquals(x, y)) return true;

                //Check whether any of the compared objects is null.
                if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                    return false;

                //Check whether the products' properties are equal.
                return x.Code == y.Code && x.DateTime == y.DateTime;
            }

            public int GetHashCode([DisallowNull] Valute obj)
            {
                //Check whether the object is null
                if (Object.ReferenceEquals(obj, null)) return 0;

                //Get hash code for the Name field if it is not null.
                int hashProductName = obj.DateTime == null ? 0 : obj.DateTime.GetHashCode();

                //Get hash code for the Code field.
                int hashProductCode = obj.Code.GetHashCode();

                //Calculate the hash code for the product.
                return hashProductName ^ hashProductCode;
            }
        }
            public struct Answer
        {
            public string code;
            public double min;
            public double max;
            public double average;
        }
        public List<Answer> Answers(string codes, DateTime start, DateTime end)
        {
            List<Answer> answers = new List<Answer>();
            foreach (var cod in codes.Split("|"))
            {
                var entity = context.Valutes.ToList().FindAll(x => x.Code == cod & (x.DateTime >= start & x.DateTime <= end));
                if (entity.Count!=0)
                {
                    var min = entity.Min(x => x.Rate / Convert.ToInt32(x.Amount));
                    var max = entity.Max(x => x.Rate / Convert.ToInt32(x.Amount));
                    var average = entity.Average(x => x.Rate / Convert.ToInt32(x.Amount));
                    answers.Add(new Answer { code = cod, min=min,max=max, average=average});
                }

            }
            return answers;
        }
    }
}
