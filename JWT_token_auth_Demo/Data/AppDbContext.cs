﻿using JWT_token_auth_Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;

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
        public DbSet<cat01menu_category> cat01menu_category { get; set; }
        public DbSet<cat02menu_sub_category> cat02menu_sub_category { get; set; }
        public DbSet<pro01product> pro01product { get; set; }
        public DbSet<pro02product_files> pro02product_files { get; set; }
        public DbSet<agent01profile> agent01profile { get; set; }
        public DbSet<agent02profile_img> agent02profile_img { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<cat01menu_category>()
                .HasMany(a => a.sub_category)
                .WithOne(b => b.cat01menu_category)
                .HasForeignKey(b => b.cat02cat01uin);

            builder.Entity<pro01product>()
               .HasMany(a => a.pro02product_files)
               .WithOne(b => b.pro01product)
               .HasForeignKey(b => b.pro02pro01uin); // in product relation with category and sub category left

            builder.Entity<agent01profile>()
               .HasMany(a => a.agent02profile_img)
               .WithOne(b => b.agent01profile)
               .HasForeignKey(b => b.agent02agent01uin);
        }
    }
}
