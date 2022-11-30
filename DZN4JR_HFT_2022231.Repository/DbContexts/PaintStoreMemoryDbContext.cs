using DZN4JR_HFT_2022231.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace DZN4JR_HFT_2022231.Repository.DbContexts
{
    public class PaintStoreMemoryDbContext: DbContext
    {
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Paint> Paints { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

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
            DeleteBehavior noAction = DeleteBehavior.NoAction;

            modelBuilder.Entity<Paint>(paint =>
                paint.HasOne(x => x.Brand).WithMany(x => x.Paints).HasForeignKey(x => x.BrandId).OnDelete(noAction));

            modelBuilder.Entity<Customer>(customer =>
                customer.HasOne(x => x.FavoritePaint).WithMany(x => x.Customers).HasForeignKey(x => x.FavoritePaintId).OnDelete(noAction) );

            var tricolor = new Brand(11111, "Tricolor", "Tricolor Kft.", "Hungary", "3531 Miskolc, Damjanich János u. 8/1", 5);
            var dulux = new Brand(22222, "Dulux", "Dulux Bt.", "Hungary", "4531 Nyírpazony, Béke u. 22/A.", 5);
            var trikkuria = new Brand(33333, "Trikkuria", "Tikkurila Oyj", "Finland", "FI-01301 Vantaa, Finland", 4);
            var hera = new Brand(44444, "Héra", "Héra Kft.", "Hungary", "2536 Nyergesújfalu, Nagy L. u. 3.", 2);

            var paints = new List<Paint>()
            {
                new Paint(1111, "falfesték", 4560, "5 liter", "101E fehér", 44444),  
                new Paint(2222, "falfesték", 6250, "6 liter", "204SG zöld", 22222),  
                new Paint(3333, "fafesték", 7590, "2.4 liter", "421KL fekete", 33333),  
                new Paint(4444, "fémfesték", 4160, "1 liter", "118D fehér", 11111),  
            };

            var geza = new Customer(1111, "Szabó Géza", "1125 Budapest Költő u. 7.", "szabogeza@gmail.com", true, 1111);
            var janos = new Customer(2222, "Horváth János", "1191 Budapest Rózsa u. 22.", "horvathjanos@gmail.com", false, 2222);
            var zoltan = new Customer(3333, "Németh Zoltán", "1162 Budapest Vak Bottyán u. 34.", "nemeth33@gmail.com", true, 3333);
            var ede = new Customer(4444, "Teller Ede", "1016 Budapest Galeotti u. 80.", "tellerede@gmail.com", true, 1111);

            modelBuilder.Entity<Brand>().HasData(tricolor, dulux, trikkuria, hera);
            modelBuilder.Entity<Paint>().HasData(paints);
            modelBuilder.Entity<Customer>().HasData(geza, janos, zoltan, ede);
            
        }
    }
}
