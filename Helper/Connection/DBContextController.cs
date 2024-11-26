using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Data
{
    public class GitNexusDBContext : DbContext
    {

        public GitNexusDBContext() { }
        public GitNexusDBContext(DbContextOptions<GitNexusDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Helper.Connection.Constants.ConnectionString;
            optionsBuilder.UseMySql(connectionString,
                ServerVersion.AutoDetect(connectionString));
        }
        //public DbSet<ServerConfig> ServerConfigs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Node> Nodes { get; set; }
    }
    public class DbConnection
    {
        public static DbContextOptions<GitNexusDBContext> GetDbContextOptions()
        {
            return new DbContextOptionsBuilder<GitNexusDBContext>()
                .UseMySql(Helper.Connection.Constants.ConnectionString,
                    ServerVersion.AutoDetect(Helper.Connection.Constants.ConnectionString))
                .Options;
        }
    }
}
