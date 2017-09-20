using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{
    public class User : BaseEntity
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }
    }
}