using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{
    public class Transaction : BaseEntity
    {
        public long Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public int users_id { get; set; }
    }
}