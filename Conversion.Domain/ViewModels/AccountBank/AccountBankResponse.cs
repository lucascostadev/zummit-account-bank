using Balance.Domain.ViewModels;
using FluentValidation.Results;

namespace Balance.Api.ViewModels.Convert
{
    public class AccountBankResponse : BaseResponse
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Balance { get; set; }

        public AccountBankResponse() { }

        public AccountBankResponse(List<ValidationFailure> errors)
        {
            Errors = errors.Select(x => x.ErrorMessage).ToList();
        }
    }
}
