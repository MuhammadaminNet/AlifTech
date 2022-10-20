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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Sender)
                .WithMany().OnDelete(DeleteBehavior.NoAction);
        
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Achiever)
                .WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
