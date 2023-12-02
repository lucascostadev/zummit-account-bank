using Conversion.Domain.ViewModels;
using FluentValidation.Results;

namespace Conversion.Api.ViewModels.Convert
{
    public class ConvertResponse : BaseResponse
    {
        public ConvertResponse() { }

        public ConvertResponse(List<ValidationFailure> errors)
        {
            Errors = errors.Select(x => x.ErrorMessage).ToList();
        }

        public decimal? ConvertedValue { get; set; }
    }
}
