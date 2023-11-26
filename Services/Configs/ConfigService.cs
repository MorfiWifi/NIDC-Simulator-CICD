using Core.Repository;
using Domain.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Models.Config;

namespace Services.Configs
{
    public partial class ConfigService : BaseService<SimulationConfig>, IConfigService
    {
        private readonly IRepository<AccountConfig> _accountConfigRep;

        public ConfigService(IRepository<SimulationConfig> repo, IRepository<AccountConfig> accountConfigRep) :
            base(repo)
        {
            _accountConfigRep = accountConfigRep;
        }

        public List<SimulationConfig> GetUserConfigs(Guid accountID, string[] roles)
        {
            if (roles != null && (roles.Contains(Consts.AdminRole) || roles.Contains(Consts.Developer)))
            {
                return GetQueryable().Where(x => x.AccountId == accountID).ToList();
            }

            var account_config_ids =
                _accountConfigRep.Where(x => x.AccountId == accountID).Select(x => x.ConfigId).ToList();
            return GetQueryable().Where(x => account_config_ids.Contains(x.Id)).ToList();
        }

        public List<AccountConfig> GetConfigsAccounts(Guid configId)
        {
            return _accountConfigRep.Where(x => x.ConfigId == configId).ToList();
        }
        public void DeleteAllConfigAccounts(Guid configId)
        {
            var accs= _accountConfigRep.Where(x => x.ConfigId == configId).ToList();
            foreach (var ac in accs)
                _accountConfigRep.Delete(x => x.ConfigId == configId);
            
        }

        public void AddOrUpdateConfigsAccounts(List<AccountConfig> accountConfigs)
        {
            var configIds = accountConfigs.Select(x => x.ConfigId).Distinct().ToList();
            var oldConfigs = _accountConfigRep.Where(x => configIds.Contains(x.ConfigId)).ToList();
            foreach (var config in oldConfigs)
            {
                _accountConfigRep.Delete(config );
            }
            
            _accountConfigRep.AddRange(accountConfigs);
            _accountConfigRep.SaveChanges();
        }
    }

    public interface IConfigService : IBaseService<SimulationConfig>
    {
        List<SimulationConfig> GetUserConfigs(Guid accountID, string[] roles);
        List<AccountConfig> GetConfigsAccounts(Guid configId );
        void AddOrUpdateConfigsAccounts(List<AccountConfig> accountConfigs );
        void DeleteAllConfigAccounts(Guid configId);

    }
}