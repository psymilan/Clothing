using System;
using System.Linq.Expressions;

namespace Clothing.Web.DataModels
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }


    public static class RoleName
    {
        public const string Customer = "Customer";

        public const string Admin = "Admin";
    }
}