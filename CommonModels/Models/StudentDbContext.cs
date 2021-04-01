using CommonModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModels
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<StudentProfile> UserProfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=192.168.0.107;Database=WebStudent;User Id=testlogin;password=testpass");
            }
        }

       

    }
}
