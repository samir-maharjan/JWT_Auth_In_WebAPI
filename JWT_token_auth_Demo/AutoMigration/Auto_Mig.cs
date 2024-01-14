using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using JWT_token_auth_Demo.Data;
using JWT_token_auth_Demo.Models;

namespace JWT_token_auth_Demo.AutoMigration
{
    public class Auto_Migration
    {
        public static void Initialize(AppDbContext appDb)
        {
            try
            {
                appDb.Database.Migrate();
            }
            catch (Exception ex)
            {
                throw new Exception("Database migration failed. Please contact support.", ex);

            }



        }
    }
}
