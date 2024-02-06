using LibraryManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.DAL.EF;
public class ApplicationDbContext : DbContext
{
    public DbSet<Library> Library { get; set; } = null!;
    public DbSet<Client> Clients { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
