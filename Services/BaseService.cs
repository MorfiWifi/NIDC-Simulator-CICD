using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Repository;
using Domain;

namespace Services
{
    public class BaseService<T>:IBaseService<T>
    where T:BaseEntity
    {
        public readonly IRepository<T> _repo;

        public BaseService(IRepository<T> repo)
        {
            _repo = repo;
        }

        public virtual void Add(T entity)
        {
            _repo.Add(entity);
        }

        public virtual void Update(T entity)
        {
            entity.ModifyDate=DateTime.Now;
            _repo.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            _repo.Delete(entity);
        }
        public virtual void Delete(Guid Id)
        {
            var entity = GetById(Id);
            _repo.Delete(entity);
        }
        public virtual IQueryable<T> GetQueryable()
        {
            return _repo.ToQueryable();
        }

        public virtual IQueryable<T> GetWithPaging(int page = 1, int pageSize = 100)
        {
            return GetQueryable().Skip(Math.Max(0, (page - 1) * pageSize)).Take(pageSize);
        }

        public virtual T GetById(Guid id)
        {
            return _repo.FirstOrDefault(id);
        }

        public virtual void AddOrUpdate(T entity)
        {
            if(entity.Id!=Guid.Empty)
                Update(entity);
            else
                Add(entity);
        }

        public void SaveChanges()
        {
            _repo.SaveChanges();
        }
    }
}
