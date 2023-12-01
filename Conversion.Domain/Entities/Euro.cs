namespace Conversion.Domain.Entities
{
    public class Euro : BaseEntity
    {
        public string Currency { get; set; } = string.Empty;
        
        public decimal Value { get; set; } = decimal.Zero;
    }
}
