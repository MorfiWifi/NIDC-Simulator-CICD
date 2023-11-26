using Core.Repository;
using Domain.Config;
using Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Configs
{
    public class SimulationService : BaseService<Simulation>, ISimulationService
    {
        private readonly IAuthService _authService;
        public SimulationService(IRepository<Simulation> repo, IAuthService authService = null) : base(repo)
        {
            _authService = authService;
        }

        public List<Simulation> GetByOrganization(Guid orgId)
        {
            return GetQueryable().Where(x=>x.Config.Account.OrganizationId == orgId).ToList();
        }

       
    }
    public interface ISimulationService:IBaseService<Simulation>
    {
        List<Simulation> GetByOrganization(Guid orgId);
     

    }
}
