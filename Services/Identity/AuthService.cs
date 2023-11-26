using Core.Repository;
using Domain.Identity;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Identity
{
    public class AuthService : BaseService<Account>, IAuthService
    {
        private readonly IRepository<AccountRole> _accountRoleRepo;
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<LoginToken> _loginTokenRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IRepository<Role> _rolRepository;
        private readonly ITokenIssuer _tokenIssuer;
        private readonly IRoleService _roleService;

        public override IQueryable<Account> GetQueryable()
        {
            return _accountRepository.ToQueryable().Include(x => x.AccountRoles);
        }
        public AuthService(IServiceProvider serviceProvider, IRepository<Role> rolRepository,
            IRepository<Account> accountRepository, IRepository<AccountRole> accountRoleRepo, IRoleService roleService) : base(accountRepository)
        {
            _rolRepository = rolRepository;
            _accountRepository = accountRepository;
            _loginTokenRepository = serviceProvider.GetService<IRepository<LoginToken>>();
            _refreshTokenRepository = serviceProvider.GetService<IRepository<RefreshToken>>();
            _tokenIssuer = serviceProvider.GetService<ITokenIssuer>();
            _accountRoleRepo = accountRoleRepo;
            _roleService = roleService;
        }




        public async Task<Account> GetByLoginToken(string username, string token, Guid organizationId)
        {
            var now = DateTime.Now;

            var loginToken = await _loginTokenRepository
                .Where(x => !x.Used && x.ExpireationDate > now)
                .Where(x => x.Token.ToLower() == token.ToLower())
                .Where(x => x.Account.Username.ToLower() == username.ToLower())
                .Where(x => x.Account.OrganizationId == organizationId)
                .Include(x => x.Account).FirstOrDefaultAsync();

            if (loginToken is null) return null;

            var account = loginToken.Account;
            return account;
        }

        public async Task<(Account account, RefreshToken refreshToken)> GetByRefreshToken(string token)
        {
            var refreshToken = await _refreshTokenRepository
              .ToQueryable().Include(x => x.LoginToken).ThenInclude(x => x.Account)
              .FirstOrDefaultAsync(x => x.Token.ToLower().Trim() == token.Trim().ToLower());

            if (refreshToken is null) return (null, null);

            return (refreshToken.LoginToken.Account, refreshToken);
        }



        public async Task<Account> GetByUsername(string username, Guid organizationId)
        {
            return await _accountRepository.
                FirstOrDefaultAsync(x => x.Username.ToLower().Trim() == username.ToLower().Trim() &&
                x.OrganizationId == organizationId);
        }

        public async Task<Account> GetByUsernameAndPassword(string username,
            string password, Guid organizationId)
        {
            return await _accountRepository.
                FirstOrDefaultAsync(x => x.Username.Equals(username) &&
                x.Password.Equals(password) && x.OrganizationId == organizationId);

        }

        public async Task<Account> GetOrCreate(string username, Guid organizationId)
        {
            var account = await GetByUsername(username, organizationId);
            if (account is null)
            {
                account = new Account
                {
                    Username = username,
                    Mobile = username,
                    Password = new Random().Next(100000, 999999).ToString()
                };
                await _accountRepository.AddAsync(account);
            }

            return account;
        }

        public async Task<LoginToken> GenerateLoginToken(Account account)
        {
            var now = DateTime.Now;
            var token = await _loginTokenRepository.FirstOrDefaultAsync(x => x.AccountId == account.Id && !x.Used && x.ExpireationDate > now);

            if (token == null)
            {
                var code = RandomGenerator.GetRandomString(4);
                token = new LoginToken
                {
                    AccountId = account.Id,
                    ExpireationDate = DateTime.Now.AddMinutes(2),
                    Token = code
                };
                await _loginTokenRepository.AddAsync(token);
            }

            return token;
        }

        public async Task<RefreshToken> GenerateRefreshToken(LoginToken loginToken)
        {
            if (loginToken.Used) return null;


            var refreshToken = loginToken.RefreshToken ?? new RefreshToken();
            loginToken.Used = true;
            refreshToken.Token = Guid.NewGuid().ToString();
            refreshToken.LoginTokenId = loginToken.Id;

            if (loginToken.RefreshToken is null)
            {
                await _refreshTokenRepository.AddAsync(refreshToken);
            }
            else
            {
                await _loginTokenRepository.SaveChangesAsync();
            }

            return refreshToken;
        }

        public async Task<PlainToken> GeneratePlainToken(Guid id, string loginToken)
        {
            var account = await _accountRepository.FirstOrDefaultAsync(id);
            if (account is null) return null;

            var token = await _loginTokenRepository
                .Where(x => x.AccountId == id)
                .Include(x => x.Account)
                .ThenInclude(x => x.AccountRoles)
                .ThenInclude(x => x.Role)
                .Include(x => x.RefreshToken)
                .FirstOrDefaultAsync(x => x.Token == loginToken);
            if (token is null) return null;

            var refreshToken = await GenerateRefreshToken(token);
            var plainToken = _tokenIssuer.IssuePlainToken(account, refreshToken.Token);

            return plainToken;
        }

        public async Task<PlainToken> GeneratePlainToken(Account account, string loginToken)
        {
            var token = await _loginTokenRepository
                .Where(x => x.AccountId == account.Id)
                .Where(x => x.Token == loginToken)
                .Include(x => x.Account)
                .ThenInclude(x => x.AccountRoles)
                .ThenInclude(x => x.Role)
                .Include(x => x.RefreshToken)
                .FirstOrDefaultAsync();
            if (token is null) return null;

            var refreshToken = await GenerateRefreshToken(token);
            if (refreshToken is null) return null;

            var plainToken = _tokenIssuer.IssuePlainToken(token.Account, refreshToken.Token);

            return plainToken;
        }

        public async Task<PlainToken> RefreshToken(string token)
        {
            var refreshToken = await _refreshTokenRepository
                .Where(x => x.Token == token)
                .Include(x => x.LoginToken)
                .ThenInclude(x => x.Account)
                .ThenInclude(x => x.AccountRoles)
                .ThenInclude(x => x.Role)
                .FirstOrDefaultAsync();

            if (refreshToken is null || !refreshToken.LoginToken.Used) return null;

            refreshToken.Token = Guid.NewGuid().ToString();
            await _refreshTokenRepository.UpdateAsync(refreshToken);

            var plainToken = _tokenIssuer.IssuePlainToken(refreshToken.LoginToken.Account, refreshToken.Token);
            return plainToken;
        }

        public List<Account> SearchUserToInvite(string search, Guid userId, Guid organizationId)
        {
            return GetQueryable().ToList()
                .Where(x => !x.ParentAccountId.HasValue && x.Id != userId
                                                                   && x.Mobile.Contains(search) && x.OrganizationId == organizationId)
                                                                       .ToList();
        }

        public List<Account> GetUsersByInviteeId(Guid inviteeId)
        {
            return GetQueryable().Where(x => x.ParentAccountId.Equals(inviteeId)).ToList();
        }

        public async Task<Account> SetUserActivation(Guid id, bool isActive)
        {
            var account = await _accountRepository.FirstOrDefaultAsync(id);
            if (account is null) return null;

            account.IsActive = isActive;
            await _accountRepository.SaveChangesAsync();

            account = await _accountRepository.FirstOrDefaultAsync(id);
            return account;
        }

        public async Task<Account> SetUserConfigs(Guid id, int configsCount)
        {
            var account = await _accountRepository.FirstOrDefaultAsync(id);
            if (account is null) return null;

            account.ConfigsCount = configsCount;
            await _accountRepository.SaveChangesAsync();

            account = await _accountRepository.FirstOrDefaultAsync(id);
            return account;
        }

        public async Task<Account> SetUserSimulationLength(Guid id, int simulationLength)
        {
            var account = await _accountRepository.FirstOrDefaultAsync(id);
            if (account is null) return null;

            account.SimulationLength = simulationLength;
            await _accountRepository.SaveChangesAsync();

            account = await _accountRepository.FirstOrDefaultAsync(id);
            return account;
        }

        public void SetDeveloperUser()
        {
            var role = _roleService.CreateIfNotExist(Consts.Developer);
            _roleService.CreateIfNotExist(Consts.AdminRole);
            _roleService.CreateIfNotExist(Consts.CoordinatorRole);
            _roleService.CreateIfNotExist(Consts.AssessorRole);
            string[] developers = { "09169212241", "09161151186" };
            var users = GetQueryable().Where(x => developers.Contains(x.Username)).ToList();
            foreach (var user in users)
            {
                if (role != null)
                {
                    var accountRole = _accountRoleRepo.FirstOrDefault(x => x.RoleId == role.Id && x.AccountId == user.Id);
                    if (accountRole == null)
                        _accountRoleRepo.Add(new AccountRole
                        {
                            AccountId = user.Id,
                            RoleId = role.Id
                        });
                }
            }
        }

        public void AssignRoleToUser(string role, Guid userId)
        {
            var user = GetById(userId);
            if (user == null)
                return;
            var roleObj = _rolRepository.FirstOrDefault(x => x.Name == role);
            if (roleObj == null)
                return;
            var Existed = _accountRoleRepo.FirstOrDefault(x => x.AccountId == userId && x.RoleId == roleObj.Id);
            if (Existed == null)
                _accountRoleRepo.Add(new AccountRole
                {
                    AccountId = userId,
                    RoleId = roleObj.Id
                });
        }

        public void RemoveUserRole(string role, Guid userId)
        {
            var user = GetById(userId);
            if (user == null)
                return;
            var roleObj = _rolRepository.FirstOrDefault(x => x.Name == role);
            if (roleObj == null)
                return;
            var Existed = _accountRoleRepo.FirstOrDefault(x => x.AccountId == userId && x.RoleId == roleObj.Id);
            if (Existed != null)
                _accountRoleRepo.Delete(Existed);
        }


    }
}
