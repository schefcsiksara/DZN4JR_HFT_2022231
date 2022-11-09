using DZN4JR_HFT_2022231.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DZN4JR_HFT_2022231.Repository.DbContexts
{
    public class PaintStoreMemoryDbContext: DbContext
    {
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Paint> Paints { get; set; }

        public PaintStoreMemoryDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("PaintStore").UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
