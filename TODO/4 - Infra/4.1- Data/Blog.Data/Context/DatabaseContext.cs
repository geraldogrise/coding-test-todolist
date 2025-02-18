using Azure;
using Todo.Data.Core.Extensions;
using Todo.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Todo.Domain.Aggreagates.Users;

namespace Todo.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Domain.Aggreagates.Tasks.Task> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new UserMap());
            modelBuilder.AddConfiguration(new TaskMap());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.EnableSensitiveDataLogging(false);
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"), opt => opt.CommandTimeout(120));
        }
    }
}
