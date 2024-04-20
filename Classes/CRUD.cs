using Newtonsoft.Json.Linq;
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
            foreach (var valute in valutes)
            {
                var entity = context.Valutes.FirstOrDefault(x => x.Code == valute.Code & (x.DateTime>=start & x.DateTime<=end));
                if (entity == null)
                {
                    _ = context.Valutes.Add(valute);
                    
                }

            }
            _ = context.SaveChanges();
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
