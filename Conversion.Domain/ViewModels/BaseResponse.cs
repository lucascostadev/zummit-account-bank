using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;

namespace Balance.Domain.ViewModels
{
    public class BaseResponse
    {
        public BaseResponse() { }

        public BaseResponse(List<ValidationFailure> errors)
        {
            Errors = errors.Select(x => x.ErrorMessage).ToList();
        }

        public List<string>? Errors { get; set; } = null;

        public bool? IsFailed
        {
            get
            {
                if (Errors == null)
                {
                    return null;
                }

                return Errors.Count > 0;
            }
        }

        public string CreatedAt { get; set; }
    }
}
