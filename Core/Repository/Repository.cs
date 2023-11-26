using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain;

namespace Core.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        #region Properties

        protected readonly DbContext dbContext;

        #endregion

        #region Ctor

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #endregion

        #region First or Default

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Where(x => !x.Deleted).FirstOrDefault(predicate);
        }

        public T FirstOrDefault(Guid id)
        {
            return dbContext.Set<T>().Where(x => !x.Deleted).FirstOrDefault(x => x.Id == id);
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Where(x => !x.Deleted).FirstOrDefaultAsync(predicate);
        }

        public Task<T> FirstOrDefaultAsync(Guid id)
        {
            return dbContext.Set<T>().Where(x => !x.Deleted).FirstOrDefaultAsync(x => x.Id == id);
        }

        #endregion

        #region All

        public IQueryable<T> ToQueryable()
        {
            return dbContext.Set<T>().Where(x => !x.Deleted);
        }

        public ICollection<T> ToList()
        {
            return ToQueryable().ToList();
        }

        public Task<List<T>> ToListAsync()
        {
            return ToQueryable().ToListAsync();
        }

        #endregion

        #region Where

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return dbContext.Set<T>().Where(x => !x.Deleted).Where(predicate);
        }

        public IQueryable<T> ToQueryable(Expression<Func<T, bool>> predicate)
        {
            return Where(predicate);
        }

        public ICollection<T> ToList(Expression<Func<T, bool>> predicate)
        {
            return ToQueryable(predicate).ToList();
        }

        public Task<List<T>> ToListAsync(Expression<Func<T, bool>> predicate)
        {
            return ToQueryable(predicate).ToListAsync();
        }

        #endregion

        #region Add

        public T Add(T o)
        {
            dbContext.Set<T>().Add(o);
            SaveChanges();
            return o;
        }

        public IList<T> AddRange(IList<T> list)
        {
            dbContext.Set<T>().AddRange(list);
            SaveChanges();
            return list;
        }

        public async Task<T> AddAsync(T o)
        {
            dbContext.Set<T>().Add(o);
            await SaveChangesAsync();
            return o;
        }

        public async Task<IList<T>> AddRangeAsync(IList<T> list)
        {
            dbContext.Set<T>().AddRange(list);
            await SaveChangesAsync();
            return list;
        }


        #endregion

        #region Update

        public T Update(T input)
        {
            dbContext.Entry(input).State = EntityState.Modified;
            dbContext.SaveChanges();
            return input;
        }

        public async Task<T> UpdateAsync(T input)
        {
            dbContext.Entry(input).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return input;
        }

        #endregion

        #region Hard Delete

        public void Delete(Guid id)
        {
            var o = FirstOrDefault(id);
            if (o == null) return;
            dbContext.Entry(o).State = EntityState.Deleted;
            SaveChanges();
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            var o = FirstOrDefault(predicate);
            if (o == null) return;
            dbContext.Entry(o).State = EntityState.Deleted;
            SaveChanges();
        }

        public void Delete(T o)
        {
            dbContext.Entry(o).State = EntityState.Deleted;
            SaveChanges();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var o = FirstOrDefault(id);
            if (o == null) return -1;
            dbContext.Entry(o).State = EntityState.Deleted;
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var o = FirstOrDefault(predicate);
            if (o == null) return -1;
            dbContext.Entry(o).State = EntityState.Deleted;
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T o)
        {
            dbContext.Entry(o).State = EntityState.Deleted;
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteRangeAsync(IQueryable<T> list)
        {
            await list.ForEachAsync(o => dbContext.Entry(o).State = EntityState.Deleted);
            dbContext.Set<T>().RemoveRange(list);
            return await SaveChangesAsync();
        }

        #endregion

        #region Soft Delete

        public void SoftDelete(Guid id)
        {
            var o = FirstOrDefault(id);
            if (o == null) return;
            o.Deleted = true;
            SaveChanges();
        }

        public void SoftDelete(Expression<Func<T, bool>> predicate)
        {
            var o = FirstOrDefault(predicate);
            if (o == null) return;
            o.Deleted = true;
            SaveChanges();
        }

        public void SoftDelete(T o)
        {
            o.Deleted = true;
            SaveChanges();
        }


        public async Task<int> SoftDeleteAsync(Guid id)
        {
            var o = FirstOrDefault(id);
            if (o == null) return -1;
            o.Deleted = true;
            return await SaveChangesAsync();
        }

        public async Task<int> SoftDeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var o = FirstOrDefault(predicate);
            if (o == null) return -1;
            o.Deleted = true;
            return await SaveChangesAsync();
        }

        public async Task<int> SoftDeleteAsync(T o)
        {
            o.Deleted = true;
            return await SaveChangesAsync();
        }

        public async Task<int> SoftDeleteRangeAsync(IQueryable<T> list)
        {
            await list.ForEachAsync(o => o.Deleted = true);
            return await SaveChangesAsync();
        }

        #endregion

        #region Save Changes

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        #endregion


    }
}
