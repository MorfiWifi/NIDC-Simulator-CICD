using Core.Repository;
using Domain.BaseData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.BaseData
{
    public class OrganizationAdminService : BaseService<OrganizationAdmin>, IOrganizationAdminService
    {
        public OrganizationAdminService(IRepository<OrganizationAdmin> repo) : base(repo)
        {
        }

        public List<OrganizationAdmin> GetAdminsByOrganizationId(Guid organizationId)
        {
            return _repo.Where(x=>x.OrganizationId==organizationId).ToList();
        }
    }

    public interface IOrganizationAdminService:IBaseService<OrganizationAdmin>
    {
        List<OrganizationAdmin> GetAdminsByOrganizationId(Guid organizationId);
    }
}
