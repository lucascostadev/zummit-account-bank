using Balance.Domain.ViewModels;
using FluentValidation.Results;

namespace Balance.Api.ViewModels.Convert
{
    public class ConvertResponse : BaseResponse
    {
        public ConvertResponse() { }

        public ConvertResponse(string from, decimal value)
        {
            ConvertedValue = $"{from} {decimal.Round(value, 4).ToString("n2")}";
        }

        public ConvertResponse(List<ValidationFailure> errors)
        {
            Errors = errors.Select(x => x.ErrorMessage).ToList();
        }

        public string ConvertedValue { get; set; }
    }
}
