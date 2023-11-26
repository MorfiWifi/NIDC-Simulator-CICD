using AutoMapper;
using Domain.BaseData;
using Domain.Config;
using Domain.Identity;
using Models;
using Models.BaseData;
using Models.Config;
using Models.Identity;

namespace Api
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Account, AccountModel>();
            CreateMap<Account, CheckLoginResponse>();

            CreateMap<Role, RoleModel>();
            CreateMap<AccountRole, AccountRoleModel>();

            CreateMap<Organization, OrganizationModel>();
            CreateMap<OrganizationModel, Organization>();

            CreateMap<Folder, FolderModel>();
            CreateMap<FolderModel, Folder>();

            CreateMap<SimulationConfig, ConfigModel>();
            CreateMap<SimulationConfig, SimulationConfig>();
            CreateMap<ConfigModel, SimulationConfig>();

            CreateMap<Unit, UnitModel>();
            CreateMap<UnitModel, Unit>();

            CreateMap<AccountConfig, AccountConfigModel>();
            CreateMap<AccountConfigModel, AccountConfig>();
        }
    }
}