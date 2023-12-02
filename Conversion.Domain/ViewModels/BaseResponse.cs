using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;

namespace Conversion.Domain.ViewModels
{
    public class BaseResponse
    {
        public BaseResponse() { }

        public BaseResponse(List<ValidationFailure> errors)
        {
            Errors = errors.Select(x => x.ErrorMessage).ToList();
        }

        public List<string> Errors { get; set; } = new List<string>();
    }
}
