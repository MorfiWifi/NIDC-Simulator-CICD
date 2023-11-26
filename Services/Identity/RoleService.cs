using Core.Repository;
using Domain.Identity;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity
{
    public class RoleService : BaseService<Role>, IRoleService
    {
       
        public RoleService( IRepository<Role> rolRepository): base(rolRepository)
        {
            
        }

        public Role CreateIfNotExist(string roleName)
        {
            var existed = _repo.Where(x => x.Name.Equals(roleName));
            if (!existed.Any())
            {
                var r = new Role { Name = roleName };
                _repo.Add(r);
                return r;
            }
            return existed.FirstOrDefault();

        }

        public Role GetByName(string roleName)
        {
            return _repo.FirstOrDefault(x=>x.Name.Equals(roleName));
        }
    }
    public interface IRoleService:IBaseService<Role>
    {
        Role GetByName(string roleName);
        Role CreateIfNotExist(string roleName);
    }
}
