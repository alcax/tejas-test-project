using Microsoft.EntityFrameworkCore;
using SampleProject.Database.Entity;
using SampleProject.Database.SeedData;
namespace SampleProject.Database;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Contact> Contact { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (Database.ProviderName== "Microsoft.EntityFrameworkCore.Sqlite")
        {
             modelBuilder.Entity<Contact>()
               .Property(e => e.Id).ValueGeneratedOnAdd().UseIdentityColumn(101110, 1);
        }
        SeedContactData.Seeddata(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }
}
