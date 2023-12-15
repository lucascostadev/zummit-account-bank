using Balance.Domain.ViewModels;

namespace Balance.Api.ViewModels.Convert
{
    public class AccountBankRequest : BaseRequest
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public decimal Balance { get; set; }
    }
}
