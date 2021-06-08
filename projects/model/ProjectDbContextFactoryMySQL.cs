using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace projects.model {
    public class ProjectDbContextFactoryMySql : IDesignTimeDbContextFactory<ProjectDbContext> {
        public ProjectDbContext CreateDbContext(string[] args) {
            
            var properties = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var optionsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();

            optionsBuilder
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .UseMySql(properties["ConnectionStrings:DefaultConnection"],
                ServerVersion.FromString("8.0.23"), null);

            return new ProjectDbContext(optionsBuilder.Options);
        }
    }
}