using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Wd3eCore.CVMDesktop
{
    /// <summary>
    /// 通用存储库
    /// </summary>
    public class GenericRepository<TEntity> where TEntity:class
    {
        internal CVMDesktop_Context context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(CVMDesktop_Context context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void BulkInsert(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null)
        {
            this.context.BulkInsert(entities);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void BatchDelete(
            Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            query.Where(filter).BatchDelete();
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual void BatchUpdate(TEntity newEntity,
            Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            query.Where(filter).BatchUpdate(m => newEntity);
        }

    }
}
