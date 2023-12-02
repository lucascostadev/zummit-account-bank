namespace Conversion.Api.ViewModels.Convert
{
    public class ConvertRequest
    {
        public string? To { get; set; }
        
        public string? From { get; set; }

        public decimal Value { get; set; }
    }
}
