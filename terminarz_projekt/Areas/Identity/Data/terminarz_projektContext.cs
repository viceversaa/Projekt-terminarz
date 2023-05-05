using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using terminarz_projekt.Areas.Identity.Data;

namespace terminarz_projekt.Data;

public class terminarz_projektContext : IdentityDbContext<UserApplication>
{
    public terminarz_projektContext(DbContextOptions<terminarz_projektContext> options)
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
}
