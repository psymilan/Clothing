using System.Linq;

namespace Clothing.Web.Data
{
    public interface IEntitySet<T> : IQueryable<T>
    {
        void Add(T obj);

        void Remove(T obj);

        T Find(int id);
    }
}
