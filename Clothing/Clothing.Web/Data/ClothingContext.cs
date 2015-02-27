using System.Data.Entity;
using Clothing.Web.DataModels;

namespace Clothing.Web.Data
{
    public class ClothingContext : DbContext
    {
        public ClothingContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UsersInRole> UsersInRoles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ItemInOrder> ItemInOrders { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Customer>().HasMany(c => c.Addresses).WithRequired().WillCascadeOnDelete(true);
        //    modelBuilder.Entity<Customer>().HasMany(c => c.CreditCards).WithRequired().WillCascadeOnDelete(true);
        //    modelBuilder.Entity<Customer>().HasMany(c => c.Orders).WithRequired().WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Order>()
        //    .HasRequired(a => a.Address)
        //    .WithMany()
        //    .HasForeignKey(u => u.CustomerId).WillCascadeOnDelete(true);
        //}

        public DbSet<Blog> Blogs { get; set; }
    }
}
