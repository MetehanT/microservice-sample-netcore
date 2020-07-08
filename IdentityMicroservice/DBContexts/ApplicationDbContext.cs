using IdentityMicroservice.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityMicroservice.DBContexts
{
	public class ApplicationDbContext : DbContext
	{

        //Veritabanı olarak SQLite kullanacağımı söylüyorum.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        //Veritabanına hazırladığım modeli tablo olarak eklemesini söylüyorum.
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        //Veritabanı oluşturulurken dummy data ile oluşturulmasını istiyorum.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = 1,
                    Username = "Admin",
                    Password = "testPassword",
                }
                );
        }
    }
}
