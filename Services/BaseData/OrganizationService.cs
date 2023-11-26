using Core.Repository;
using Domain.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BaseData
{
    public class OrganizationService : BaseService<Organization>, IOrganizationService
    {
        public OrganizationService(IRepository<Organization> repo) : base(repo)
        {
        }

        public Organization GetOrganizationByUrl(string url)
        {
            return GetQueryable().FirstOrDefault(x=>x.UniqueUrl.ToLower()== url.ToLower());
        }
    }

    public interface IOrganizationService:IBaseService<Organization>
    {
        Organization GetOrganizationByUrl(string url);
    }
}
