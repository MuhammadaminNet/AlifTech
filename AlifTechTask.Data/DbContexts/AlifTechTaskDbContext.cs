using AlifTechTask.Domain.Models.Transactions;
using AlifTechTask.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace AlifTechTask.Data.DbContexts
{
    public class AlifTechTaskDbContext : DbContext
    {
        public AlifTechTaskDbContext(DbContextOptions<AlifTechTaskDbContext> options) : base(options)
        {}

        /// <summary>
        /// User table
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Transaction table
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Wallets table
        /// </summary>
        public DbSet<Wallet> Wallets { get; set; }
    }
}
