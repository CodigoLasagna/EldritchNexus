using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Data
{
    public class SurveyDbContext : DbContext
    {

        public SurveyDbContext() { }
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Helper.Connection.Constants.ConnectionString;
            optionsBuilder.UseMySql(connectionString,
                ServerVersion.AutoDetect(connectionString));
        }
        public DbSet<ServerConfig> ServerConfigs { get; set; }
        
    }
    public class DbConnection
    {
        public static DbContextOptions<SurveyDbContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<SurveyDbContext>()
                .UseMySql(Helper.Connection.Constants.ConnectionString,
                    ServerVersion.AutoDetect(Helper.Connection.Constants.ConnectionString))
                .Options;
        }
    }
}
