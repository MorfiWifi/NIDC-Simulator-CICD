using Core.Repository;
using Domain.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Configs
{
    public class UnitService : BaseService<Unit>, IUnitService
    {
        private readonly IAccountUnitService _accountUnitService;

        public UnitService(IRepository<Unit> repo , IAccountUnitService accountUnitService) : base(repo)
        {
            _accountUnitService = accountUnitService;
        }

        public List<Unit> GetDefaultUnits()
        {
            return GetQueryable().OrderBy(x=>x.Category).Where(x => x.IsDefault == true).ToList();
        }

        public List<Unit> GetAllUnits()
        {
            return GetQueryable().OrderBy(x=>x.Category).ToList();
        }

        public void UpdateUserUnits(List<Unit> models, Guid accountId)
        {
            var unitIds = models.GroupBy(x => x.Category).Select(x=>x.First().Id).ToList();
            _accountUnitService.DeleteUserAccountUnits(accountId);
            _accountUnitService.AddUserAccountUnits(unitIds , accountId);
        }


        public List<Unit> GetUserUnits(Guid accountID)
        {
            var userUnitIds = _accountUnitService.GetUserAccountUnits(accountID).Select(x=>x.UnitId).Distinct().ToList();
            var userUnits = GetQueryable().Where(x => userUnitIds.Contains(x.Id)).ToList();
            var default_units = GetDefaultUnits();

            foreach (var unit in default_units)
            {
                if (userUnits.Any(x=>x.Category == unit.Category))
                    continue;
                
                userUnits.Add(unit);
            }
            
            return userUnits.OrderBy(x=>x.Category).ToList();
        }
    }
    public interface IUnitService:IBaseService<Unit>
    {
        List<Unit> GetUserUnits(Guid accountID);
        List<Unit> GetDefaultUnits();
        List<Unit> GetAllUnits();
        void UpdateUserUnits(List<Unit> domainModels, Guid accountId);
    }
}