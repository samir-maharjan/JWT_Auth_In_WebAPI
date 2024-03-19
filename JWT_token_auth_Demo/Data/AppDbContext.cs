using JWT_token_auth_Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace JWT_token_auth_Demo.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<OTPVerificationLogs> OTPVerificationLogs { get; set; }
        public DbSet<usr01users> usr01users { get; set; }
        public DbSet<car01caruosel> car01caruosel { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
