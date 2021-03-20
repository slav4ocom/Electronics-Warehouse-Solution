﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Console_Manager.Models
{
    public class ElectronicsWarehouseContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=192.168.0.107;Database=ElectronicsWarehouse;User Id=testlogin;password=testpass");
            }
        }

    }
}
