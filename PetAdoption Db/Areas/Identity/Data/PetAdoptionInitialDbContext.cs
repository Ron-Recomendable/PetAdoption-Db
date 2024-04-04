using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetAdoption_Db.Areas.Identity.Data;
using PetAdoption_Db.Models;

namespace PetAdoption_Db.Areas.Identity.Data;

public class PetAdoptionInitialDbContext : IdentityDbContext<PetAdoption_DbUser>
{
    public PetAdoptionInitialDbContext(DbContextOptions<PetAdoptionInitialDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<PetAdoption_Db.Models.Shelter> Shelter { get; set; } = default!;

public DbSet<PetAdoption_Db.Models.Pet> Pet { get; set; } = default!;

public DbSet<PetAdoption_Db.Models.User> User { get; set; } = default!;

public DbSet<PetAdoption_Db.Models.Application> Application { get; set; } = default!;

public DbSet<PetAdoption_Db.Models.Favorite> Favorite { get; set; } = default!;
}
