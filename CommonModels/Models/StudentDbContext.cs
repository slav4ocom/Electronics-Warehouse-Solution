using CommonModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModels
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.json", true, true)
                    .Build();
                var str = config["ConnectionStrings:DefaultConnection"];
                //optionsBuilder.UseSqlServer("Server=192.168.0.107;Database=WebStudent;User Id=testlogin;password=testpass");
                optionsBuilder.UseSqlServer(str);
            }
        }

       

    }
}
