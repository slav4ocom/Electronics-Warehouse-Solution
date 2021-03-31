using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonModels
{
    public class ElectronicsWarehouseContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=192.168.0.107;Database=ElectronicsWarehouse;User Id=testlogin;password=testpass");
                optionsBuilder.UseSqlServer("Server=192.168.0.107;Database=WebManagerV2;User Id=testlogin;password=testpass");
            }
        }

    }
}
