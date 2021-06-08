using Microsoft.EntityFrameworkCore;

namespace projects.model {
    public class ProjectDbContext : DbContext {

        public DbSet<AProject> Projects { get; set; }
        public DbSet<RequestFundingProject> RequestFundingProjects { get; set; }
        public DbSet<ResearchFundingProject> ResearchFundingProjects { get; set; }
        public DbSet<Subproject> Subprojects { get; set; }
        public DbSet<AFacility> Facilities { get; set; }
        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Debitor> Debitors { get; set; }
        public DbSet<Funding> Fundings { get; set; }
        
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) {
            
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<AProject>()
                .HasIndex(p => p.Title)
                .IsUnique(true);

            builder.Entity<Subproject>()
                .HasOne(s => s.Project)
                .WithMany()
                .HasForeignKey(s => s.ProjectId);

            builder.Entity<Subproject>()
                .HasOne(s => s.Institute)
                .WithMany()
                .HasForeignKey(s => s.InstituteId);

            builder.Entity<AFacility>()
                .HasIndex(f => f.FacilityCode)
                .IsUnique(true);

            builder.Entity<AFacility>()
                .HasIndex(f => f.FacilityTitle)
                .IsUnique(true);

            builder.Entity<AFacility>()
                .HasDiscriminator<string>("FACILITY_TYPE")
                .HasValue<Institute>("INSTITUTE")
                .HasValue<Faculty>("FACULTY");

            builder.Entity<Institute>()
                .HasOne(i => i.Faculty)
                .WithMany();

            builder.Entity<Debitor>()
                .HasIndex(d => d.Name)
                .IsUnique(true);

            builder.Entity<Funding>()
                .HasKey(f => new {f.ProjectId, f.DebitorId});
            
            builder.Entity<Funding>()
                .HasOne(f => f.Project)
                .WithMany()
                .HasForeignKey(f => f.ProjectId);

            builder.Entity<Funding>()
                .HasOne(f => f.Debitor)
                .WithMany()
                .HasForeignKey(f => f.DebitorId);

            builder.Entity<Funding>()
                .Property(f => f.Amount)
                .HasPrecision(10, 2);

        }
    }
}