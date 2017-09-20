using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{
    public class Login : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}