using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clothing.Web.DataModels
{
    public class UsersInRole
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int CustomerId { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}