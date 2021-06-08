using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using projects.model;

namespace projects.unittests {
    public class Tests {
        private DbContextOptions<ProjectDbContext> dbContextOptions =
            new DbContextOptionsBuilder<ProjectDbContext>().UseSqlite(CreateInMemoryDatabase()).Options;

        private readonly DbConnection _connection;

        private static DbConnection CreateInMemoryDatabase() {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            return connection;
        }

        public void Dispose() => _connection.Dispose();

        private ProjectDbContext CreateDbContext() {
            return new ProjectDbContext(dbContextOptions);
        }

        [SetUp]
        public void Setup() {
            CreateDbContext().Database.Migrate();
        }

    

        [Test]
        public void Test1() {
            RequestFundingProject project;
            
            using (var context = CreateDbContext()) {
                project = new RequestFundingProject
                    {Title = "Finite Elemente", LegalFoundation = ELegalFoundation.P_27, IsSmallProject = false};

                context.Projects.Add(project);
                context.SaveChanges();
                
                Assert.NotNull(project.Id);
                Console.WriteLine(project.Id);
            }
            
        }
    }
}