using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Identity;
using Models.Identity;

namespace Services.Identity
{
    public interface IAuthService:IBaseService<Account>
    {        
        Task<Account> GetByUsername(string username,Guid organizationId);
        Task<Account> GetByUsernameAndPassword(string username,string password,Guid organizationId);
        Task<Account> GetOrCreate(string username,Guid organizationId);
        Task<Account> GetByLoginToken(string username, string token,Guid organizationId);

        Task<LoginToken> GenerateLoginToken(Account account);
        Task<RefreshToken> GenerateRefreshToken(LoginToken loginToken);
        Task<(Account account, RefreshToken refreshToken)> 
            GetByRefreshToken(string token);
        
        Task<PlainToken> GeneratePlainToken
            (Account account, string token);

        Task<PlainToken> RefreshToken(string token);

        List<Account> SearchUserToInvite(string search,Guid userId,Guid organizationId);
        List<Account> GetUsersByInviteeId(Guid inviteeId);
        Task<Account> SetUserActivation(Guid userId , bool isActive);
        Task<Account> SetUserConfigs(Guid userId ,int configsCount);
        Task<Account> SetUserSimulationLength(Guid userId ,  int simulationLength);
        
        
        void SetDeveloperUser(); 
        void AssignRoleToUser(string role,Guid userId);
        void RemoveUserRole(string role, Guid userId);

    }
}
