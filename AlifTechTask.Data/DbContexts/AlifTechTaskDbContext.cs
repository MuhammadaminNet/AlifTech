using AlifTechTask.Domain.Models.Transactions;
using AlifTechTask.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace AlifTechTask.Data.DbContexts
{
    public class AlifTechTaskDbContext : DbContext
    {
        public AlifTechTaskDbContext(DbContextOptions<AlifTechTaskDbContext> options) : base(options)
        {}

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
    }
}
