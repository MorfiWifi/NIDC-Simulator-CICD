using Core.Repository;
using Domain.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Configs
{
    public class AccountUnitService : BaseService<AccountUnit>, IAccountUnitService
    {
        public AccountUnitService(IRepository<AccountUnit> repo) : base(repo)
        {
        }
        
        public List<AccountUnit> GetUserAccountUnits(Guid accountID)
        {
            return GetQueryable().Where(x=> x.AccountId == accountID).ToList();
        }

        public void DeleteUserAccountUnits(Guid accountID)
        {
            var accountUnits = GetUserAccountUnits(accountID);
            foreach (var accountUnit in accountUnits)
            {
                Delete(accountUnit);
            }
            SaveChanges();
        }

        public void AddUserAccountUnits(List<Guid> unitIds, Guid accountID)
        {
            foreach (var unitId in unitIds)
            {
                Add(new AccountUnit() { UnitId = unitId , AccountId = accountID});
            }
            SaveChanges();
        }
    }
    public interface IAccountUnitService:IBaseService<AccountUnit>
    {
        List<AccountUnit> GetUserAccountUnits(Guid accountID);
        void DeleteUserAccountUnits(Guid accountID);
        void AddUserAccountUnits(List<Guid> unitIds ,Guid accountID);
    }
}