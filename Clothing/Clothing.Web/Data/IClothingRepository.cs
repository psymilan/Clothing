using System.Data.Entity.Infrastructure;
using Clothing.Web.DataModels;

namespace Clothing.Web.Data
{
    public interface IClothingRepository
    {
        IEntitySet<Order> Orders { get; }
        IEntitySet<Customer> Customers { get;  }
        IEntitySet<CreditCard> CreditCards { get; }
        IEntitySet<Role> Roles { get; }
        IEntitySet<UsersInRole> UsersInRoles { get; }
        IEntitySet<Product> Products { get; }
        IEntitySet<ProductImage> ProductImages { get; }

        void SaveChanges();
        void Dispose();

        DbEntityEntry Entry(object product);
    }
}
