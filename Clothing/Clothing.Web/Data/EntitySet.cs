using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Clothing.Web.Data
{
    public class EntitySet<T> : IEntitySet<T> where T : class
    {
        protected DbContext DbContext { get; private set; }

        protected DbSet<T> DbSet { get; private set; }

        public EntitySet(DbContext dbContext)
        {
            #region Arguments validation

            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }

            #endregion

            DbContext = dbContext;

            DbSet = dbContext.Set<T>();
        }

        public void Add(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            DbSet.Add(obj);
        }

        public void Remove(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            DbSet.Remove(obj);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return DbSet.AsQueryable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return DbSet.AsQueryable().GetEnumerator();
        }

        public Expression Expression
        {
            get { return DbSet.AsQueryable().Expression; }
        }

        public Type ElementType
        {
            get { return DbSet.AsQueryable().ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return DbSet.AsQueryable().Provider; }
        }


        public T Find(int id)
        {
            return DbSet.Find(id);
        }
    }
}