using Microsoft.EntityFrameworkCore;

namespace AlifTechTask.Data.DbContexts
{
    public class AlifTechTaskDbContext : DbContext
    {
        public AlifTechTaskDbContext(DbContextOptions<AlifTechTaskDbContext> options) : base(options)
        {
        }




    }
}
