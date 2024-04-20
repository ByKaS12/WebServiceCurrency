namespace WebServiceCurrency.Models
{
    public class Valute
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public string? Country { get; set; }
        public string? Currency { get; set; }
        public string? Amount { get; set; }
        public string? Code { get; set; }
        public double Rate { get; set; }
    }
}
