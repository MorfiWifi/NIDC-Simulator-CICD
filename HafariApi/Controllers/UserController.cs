using Domain.Identity;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Models.Identity;
using Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Models;
using Microsoft.AspNetCore.Hosting;
using Services.BaseData;

namespace Api.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]

    public class UserController : BaseController
    {
        private readonly IAuthService authService;
        private readonly IOrganizationService _organizationService;
        public UserController(IServiceProvider serviceProvider, IMapper mapper, IWebHostEnvironment web, IOrganizationService organizationService) : base(mapper, web)
        {
            authService = serviceProvider.GetService<IAuthService>();
            _organizationService = organizationService;
        }

        [Authorize(Roles = Consts.AdminOrDeveloper)]
        [HttpGet]
        [ProducesResponseType(typeof(List<AccountModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll(Guid? organizationId)
        {
            var user = authService.GetById(CustomUser.Id);
            if (User.IsInRole(Consts.AdminRole) || (User.IsInRole(Consts.Developer) && !organizationId.HasValue))
                organizationId = user.OrganizationId;
            var users = authService.GetQueryable().Where(x => x.OrganizationId == organizationId).ToList();
            var list = Mapper.Map<List<AccountModel>>(users);
            return Ok(list);
        }

        [Authorize(Roles = Consts.AdminOrDeveloper)]
        [HttpGet]
        [ProducesResponseType(typeof(List<AccountModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllByOrganiztionUrl(string organizationUrl)
        {
            var user = authService.GetById(CustomUser.Id);
            var users = authService.GetQueryable().Where(x => x.Organization.UniqueUrl.ToLower() == organizationUrl.ToLower()).ToList();
            var list = Mapper.Map<List<AccountModel>>(users);
            return Ok(list);
        }



        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<AccountModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetMyInvitedUsers()
        {
            var users = authService.GetUsersByInviteeId(CustomUser.Id);
            return Ok(Mapper.Map<List<AccountModel>>(users));
        }


        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InviteUserToOurCompany(Guid userId)
        {
            var inviter = authService.GetById(CustomUser.Id);
            var user = authService.GetById(userId);
            if (user.ParentAccountId.HasValue)
                return BadRequest();
            user.ParentAccountId = inviter.ParentAccountId ?? CustomUser.Id;
            authService.Update(user);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<AccountModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchFreeUsers(string mobileToSearch, string organizaiotnUrl)
        {
            var organization = _organizationService.GetOrganizationByUrl(organizaiotnUrl);

            var freeUsers = authService.SearchUserToInvite(mobileToSearch, CustomUser.Id, organization.Id);
            return Ok(Mapper.Map<List<AccountModel>>(freeUsers));
        }

        [Authorize(Roles = Consts.Developer)]
        [HttpGet]
        [ProducesResponseType(typeof(List<AccountModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchUsers(string textToSearch)
        {
            var freeUsers = authService.GetQueryable().Where(x => x.LastName.Contains(textToSearch) || x.FirstName.Contains(textToSearch) ||
            x.Mobile.Contains(textToSearch)).ToList();
            return Ok(Mapper.Map<List<AccountModel>>(freeUsers));
        }


        [Authorize(Roles = Consts.Developer)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ConvertToRole(Guid userId, [FromQuery] string role)
        {
            if (role == Consts.AdminRole)
                authService.AssignRoleToUser(role: Consts.AdminRole, userId: userId);
            else if (role == Consts.AssessorRole)
                authService.AssignRoleToUser(role: Consts.AssessorRole, userId: userId);
            else if (role == Consts.CoordinatorRole)
                authService.AssignRoleToUser(role: Consts.CoordinatorRole, userId: userId);
            return Ok();
        }






        [Authorize(Roles = Consts.Developer)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> RemoveRole(Guid userId, [FromQuery] string role)
        {
            if (role == Consts.AdminRole)
                authService.RemoveUserRole(role: Consts.AdminRole, userId: userId);
            else if (role == Consts.AssessorRole)
                authService.RemoveUserRole(role: Consts.AssessorRole, userId: userId);
            else if (role == Consts.CoordinatorRole)
                authService.RemoveUserRole(role: Consts.CoordinatorRole, userId: userId);
            return Ok();
        }


        [Authorize(Roles = Consts.Developer)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SetUserActivation([FromQuery] Guid userId, [FromBody] bool isActive)
        {
            var account = await authService.SetUserActivation(userId, isActive);
            var accountModel = Mapper.Map<AccountModel>(account);
            return Ok(accountModel);
        }

        [Authorize(Roles = Consts.Developer)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SetUserConfigs([FromQuery] Guid userId, [FromQuery] string organizationUrl, [FromBody] int configsCount)
        {


            var account = await authService.SetUserConfigs(userId, configsCount);
            var accountModel = Mapper.Map<AccountModel>(account);
            return Ok(accountModel);
        }

        [Authorize(Roles = Consts.Developer)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SetUserSimulationLength([FromQuery] Guid userId, [FromQuery] string organizationUrl, [FromBody] int simulationLength)
        {
            var organization = _organizationService.GetOrganizationByUrl(organizationUrl);

            var account = await authService.SetUserSimulationLength(userId, simulationLength);
            var accountModel = Mapper.Map<AccountModel>(account);
            return Ok(accountModel);
        }
    }
}
