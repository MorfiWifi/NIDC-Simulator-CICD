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
    public class SimulationUserService : BaseService<SimulationUser>, ISimulationUserService
    {
        private readonly IAuthService authService;
        private readonly ISimulationService simulationService;
        public SimulationUserService(IRepository<SimulationUser> repo, IAuthService authService = null, ISimulationService simulationService = null) : base(repo)
        {
            this.authService = authService;
            this.simulationService = simulationService;
        }

        public List<SimulationUser> GetSimulationUsers(Guid simulationId)
        {
            return GetQueryable().Where(x=>x.SimulationId == simulationId).ToList();
        }

        public void SetUserRoleInSimulation(Guid userId, Guid SimulationId, string roleName)
        {
            var user=authService.GetById(userId);
            var simulation = simulationService.GetById(SimulationId);
            if (simulation.Config.Account.OrganizationId != user.OrganizationId)
                return;
            var existed = GetQueryable().Where(x => x.SimulationId == SimulationId && x.AccountId == userId);
            if(existed.Any())
                Delete(existed.FirstOrDefault());
            if (string.IsNullOrEmpty(roleName))
                return;
            Add(new SimulationUser
            {
                AccountId = userId,
                SimulationId = SimulationId,
                UserRole = roleName
            });
        }
        public List<Simulation> GetByUserId(Guid userId, bool isAdmin = false)
        {
            var user = authService.GetById(userId);
            if (isAdmin)
                return simulationService.GetByOrganization(user.OrganizationId.Value);
            var sims = GetQueryable().Where(x => x.AccountId == userId);
            return sims.Select(x=>x.Simulation).Distinct().ToList();
        }
        public void DeleteAllUsers(Guid simulationId)
        {
            var simulationUsersIds=GetQueryable().Where(x=>x.SimulationId==simulationId).Select(x=>x.Id).ToList();
            foreach (var simulationUser in simulationUsersIds)
            {
                Delete(simulationUser);
            }
        }

    }
    public interface ISimulationUserService : IBaseService<SimulationUser>
    {
        List<SimulationUser> GetSimulationUsers(Guid simulationId); 
        void SetUserRoleInSimulation(Guid userId,Guid SimulationId,string roleName);
        List<Simulation> GetByUserId(Guid userId, bool isAdmin = false);
        void DeleteAllUsers(Guid simulationId);

    }
}
