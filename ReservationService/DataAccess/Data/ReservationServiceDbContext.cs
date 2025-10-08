using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class ReservationServiceDbContext : IdentityDbContext<User>
    {
        public ReservationServiceDbContext()
        {
            //Database.EnsureCreated();
        }
        public ReservationServiceDbContext(DbContextOptions<ReservationServiceDbContext> options) : base(options) { }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ShopPv421;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        //}

        //    protected override void OnModelCreating(ModelBuilder modelBuilder)
        //    {
        //        base.OnModelCreating(modelBuilder);

        //        //DbInitializer.SeedCategories(modelBuilder);
        //        //DbInitializer.SeedProducts(modelBuilder);

        //        modelBuilder.SeedCategories();
        //        modelBuilder.SeedProducts();

        //        // TODO: move to separate class
        //        modelBuilder.Entity<OrderDetails>().HasOne(x => x.Order).WithMany(x => x.Items).HasForeignKey(x => x.OrderId);
        //        modelBuilder.Entity<OrderDetails>().HasOne(x => x.Product).WithMany(x => x.Orders).HasForeignKey(x => x.ProductId);
        //    }
        //}
    }
}
