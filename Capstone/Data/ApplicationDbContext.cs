using Capstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<ApplicationRole> ApplicationRoles { get; set; }

    public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        modelBuilder.Entity<ApplicationUserRole>().HasOne(a => a.ApplicationUser).WithMany(u => u.UserRoles).HasForeignKey(a => a.UserId);

        modelBuilder.Entity<ApplicationUserRole>().HasOne(a => a.ApplicationRole).WithMany(r => r.UserRoles).HasForeignKey(a => a.RoleId);

        var adminId = Guid.NewGuid().ToString();
        var customerId = Guid.NewGuid().ToString();

        modelBuilder.Entity<ApplicationRole>().HasData(
            new ApplicationRole
            {
                Id = adminId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = adminId
            },
             new ApplicationRole
             {
                 Id = customerId,
                 Name = "Customer",
                 NormalizedName = "CUSTOMER",
                 ConcurrencyStamp = customerId
             }

        );
    }
}
}
    
