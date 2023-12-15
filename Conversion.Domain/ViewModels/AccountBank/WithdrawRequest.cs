namespace Balance.Domain.ViewModels.AccountBank
{
    public class WithdrawRequest
    {
        public int AccountBankId { get; set; }

        public decimal? WithdrawValue { get; set; }
    }
}
