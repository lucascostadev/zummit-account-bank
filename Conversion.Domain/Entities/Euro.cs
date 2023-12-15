using System.Globalization;

namespace Balance.Domain.Entities
{
    public class Euro : BaseEntity
    {
        public Euro() { }

        public Euro(string currency, string rate)
        {
            Currency = currency;
            Value = Convert.ToDecimal(rate, CultureInfo.InvariantCulture);
            CreatedAt = DateTime.Now;
        }

        public string Currency { get; set; } = string.Empty;

        public decimal Value { get; set; } = decimal.Zero;

        public DateTime CreatedAt { get; set; }
    }
}
