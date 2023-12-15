using System.ComponentModel.DataAnnotations;

namespace Balance.Domain.Entities
{
    public class AccountBank : BaseEntity
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public decimal Balance { get; set; }

        public bool CanWithdraw(decimal withdrawValue)
        {
            return Balance - withdrawValue >= 0;
        }

        public string RetornaDataAberturaFormatada()
        {
            if (!CreatedAt.HasValue)
                return "N/A";

            return CreatedAt.Value.ToString("dd/MM/yyyy");
        }

        public string RetornaSaldoFormatado()
        {
            return Balance.ToString("c");
        }
    }
}
