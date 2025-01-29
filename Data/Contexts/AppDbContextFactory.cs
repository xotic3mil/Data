using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Server = 85.229.113.248; Database = postpi; User Id = postpi; Password = !a*!FE^bKr$84RRg;");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
    
}

