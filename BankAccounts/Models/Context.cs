using BankAccounts.Models;
using Microsoft.EntityFrameworkCore;
 
namespace BankAccounts.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Transaction> transactions { get; set; }
    }
}