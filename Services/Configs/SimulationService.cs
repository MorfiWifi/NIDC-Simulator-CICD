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
        private readonly IConfigService _configService;

        public SimulationService(IRepository<Simulation> repo, IConfigService configService, IAuthService authService = null) : base(repo)
        {
            _authService = authService;
            _configService = configService;
        }

        public List<Simulation> GetByOrganization(Guid orgId)
        {
            return GetQueryable().Where(x=>x.Config.Account.OrganizationId == orgId).ToList();
        }

        /// <summary>
        /// including configurations for all simulations
        /// todo change this code so it loads if required
        /// </summary>
        /// <param name="id">GUID</param>
        public override Simulation GetById(Guid id)
        {
            var trimmedSim =  base.GetById(id);
            var config =  _configService.GetById(trimmedSim.ConfigId);
            trimmedSim.Config = config;
            trimmedSim.ConfigDetails = config.ConfigDetails;

            return trimmedSim;
        }
        
    }
    public interface ISimulationService:IBaseService<Simulation>
    {
        List<Simulation> GetByOrganization(Guid orgId);
     

    }
}
