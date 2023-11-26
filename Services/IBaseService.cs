using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Services
{
    public interface IBaseService<T>
    where T:BaseEntity
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Guid Id);

        IQueryable<T> GetQueryable();
        IQueryable<T> GetWithPaging(int page = 1,int pageSize=100);
        T GetById(Guid id);
        void AddOrUpdate(T entity);
        void SaveChanges();             
    }
}
