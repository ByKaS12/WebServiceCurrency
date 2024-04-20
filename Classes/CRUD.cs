using System.Runtime.InteropServices;
using WebServiceCurrency.Models;

namespace WebServiceCurrency.Classes
{
    public class CRUD
    {
        private readonly ApplicationContext context;
        public CRUD(ApplicationContext Context) {
            context = Context;

        }
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
                _ = context.Valutes.Update(entity);
            }
        }
    }
}
