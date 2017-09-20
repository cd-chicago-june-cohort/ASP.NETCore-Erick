using System.ComponentModel.DataAnnotations;
namespace BankAccounts.Models
{
    public class TransactionViewModel : BaseEntity
    {
        [Required]
        [Display(Name = "Deposit/Withdraw")]
        public double Amount { get; set; }
    }
}