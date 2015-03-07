using System.Data.Entity.Infrastructure;
using Clothing.Web.DataModels;

namespace Clothing.Web.Data
{
    public class ClothingRepository : IClothingRepository
    {
        private readonly ClothingContext _ctx;

        public IEntitySet<Customer> Customers { get; private set; }

        public IEntitySet<Role> Roles { get; private set; }

        public IEntitySet<Order> Orders { get; private set; }

        public IEntitySet<CreditCard> CreditCards { get; private set; }

        public IEntitySet<UsersInRole> UsersInRoles { get; private set; }

        public IEntitySet<Product> Products { get; private set; }

        public IEntitySet<ProductImage> ProductImages  { get; private set; }

        public IEntitySet<ItemInOrder> ItemInOrders { get; private set; }

        public IEntitySet<ProImage> ProImages { get; private set; }


        public ClothingRepository(ClothingContext ctx)
        {
            _ctx = ctx;

            Roles = new EntitySet<Role>(ctx);

            Customers = new EntitySet<Customer>(ctx);

            Orders = new EntitySet<Order>(ctx);

            CreditCards = new EntitySet<CreditCard>(ctx);

            UsersInRoles = new EntitySet<UsersInRole>(ctx);

            Products = new EntitySet<Product>(ctx);

            ProductImages = new EntitySet<ProductImage>(ctx);

            ItemInOrders = new EntitySet<ItemInOrder>(ctx);

            ProImages = new EntitySet<ProImage>(ctx);

        }


        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public DbEntityEntry Entry(object entity)
        {
            return _ctx.Entry(entity);
        }
    }
}