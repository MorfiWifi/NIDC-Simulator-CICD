using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IRepository<T>
    {
        #region First or Default

        T FirstOrDefault(Guid id);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        Task<T> FirstOrDefaultAsync(Guid id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        #endregion

        #region  Get All

        IQueryable<T> ToQueryable();
        ICollection<T> ToList();

        Task<List<T>> ToListAsync();

        #endregion

        #region Where

        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        IQueryable<T> ToQueryable(Expression<Func<T, bool>> predicate);
        ICollection<T> ToList(Expression<Func<T, bool>> predicate);

        Task<List<T>> ToListAsync(Expression<Func<T, bool>> predicate);

        #endregion

        #region Add

        T Add(T o);
        IList<T> AddRange(IList<T> list);

        Task<T> AddAsync(T o);
        Task<IList<T>> AddRangeAsync(IList<T> list);

        #endregion

        #region Update

        T Update(T entity);

        Task<T> UpdateAsync(T entity);

        #endregion

        #region Hard Delete

        void Delete(Guid id);
        void Delete(T o);
        void Delete(Expression<Func<T, bool>> predicate);

        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteAsync(T o);
        Task<int> DeleteAsync(Expression<Func<T, bool>> predicate);
        Task<int> DeleteRangeAsync(IQueryable<T> list);

        #endregion

        #region Soft Delete

        void SoftDelete(Guid id);
        void SoftDelete(T o);
        void SoftDelete(Expression<Func<T, bool>> predicate);

        Task<int> SoftDeleteAsync(Guid id);
        Task<int> SoftDeleteAsync(T o);
        Task<int> SoftDeleteAsync(Expression<Func<T, bool>> predicate);
        Task<int> SoftDeleteRangeAsync(IQueryable<T> list);

        #endregion

        #region Save Changes

        int SaveChanges();

        Task<int> SaveChangesAsync();

        #endregion


    }
}
