using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Monitoring.Core.Models;
using System.IO;

namespace Monitoring.Core
{
    public class MonitoringContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentDashboard> DepartmentDashbords { get; set; }

        public MonitoringContext(DbContextOptions<MonitoringContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Department>(x =>
            {
                x.ToTable(nameof(Department));
                x.HasKey(z => z.Id);
                x.HasMany(z => z.DepartmentDashbords).WithOne(s => s.Department).HasForeignKey(s => s.DepartmentId);
            });

            modelBuilder.Entity<DepartmentDashboard>(x =>
            {
                x.ToTable(nameof(DepartmentDashboard));
                x.HasKey(z => z.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }

    //public class MonitoringContextFactory : IDesignTimeDbContextFactory<MonitoringContext>
    //{
    //    public MonitoringContext CreateDbContext(string[] args)
    //    {
    //        var configuration = new ConfigurationBuilder()
    //             .SetBasePath(Directory.GetCurrentDirectory())
    //             .AddJsonFile("appsettings.json")
    //             .Build();

    //        var dbContextBuilder = new DbContextOptionsBuilder<MonitoringContext>();

    //        var connectionString = configuration
    //                    .GetConnectionString("SqlConnectionString");

    //        dbContextBuilder.UseSqlServer(connectionString);

    //        return new MonitoringContext(dbContextBuilder.Options);
    //    }
    //}

    public class MonitoringContextFactory : IDesignTimeDbContextFactory<MonitoringContext>
    {
        public MonitoringContextFactory()
        {
        }

        private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        public MonitoringContext CreateDbContext(string[] args)
        {

            var builder = new DbContextOptionsBuilder<MonitoringContext>();
            builder.UseSqlServer(Configuration.GetConnectionString("SqlConnectionString"));

            return new MonitoringContext(builder.Options);
        }
    }

}
