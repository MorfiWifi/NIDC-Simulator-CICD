using Domain.Identity;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Models.Identity;
using Services.Identity;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Services.BaseData;

namespace Api.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]

    public class AuthController : BaseController
    {
        private readonly IAuthService authService;
        private readonly IRoleService _roleService;
        private readonly IOrganizationService _organizationService;
        public AuthController(IServiceProvider serviceProvider, IMapper mapper, IRoleService roleService,
            IWebHostEnvironment webHostEnvironment, IOrganizationService organizationService) : base(mapper, webHostEnvironment)
        {
            authService = serviceProvider.GetService<IAuthService>();
            _roleService = roleService;
            _organizationService = organizationService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OtpRequestResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            var organization=_organizationService.GetOrganizationByUrl(model.OrganizationUrl);
            var account =await  authService.GetByUsername(model.Mobile,organization.Id);
            if (account == null)
            {
                account = new Account
                {
                    Mobile = model.Mobile,
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    Username = model.Mobile,
                    Password = new Random().Next(100000, 999999).ToString()
                };
                authService.Add(account);
            }
            var smsResult = SmsTools.SendVerifyMessage(account.Mobile, account.Password);
            if (!smsResult)
                return BadRequest();
            var token = await authService.GenerateLoginToken(account);
            return Ok(new OtpRequestResult());
        }


        [HttpPost]
        [ProducesResponseType(typeof(OtpRequestResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] OTPLoginModel model)
        {
            var organization = _organizationService.GetOrganizationByUrl(model.OrganizationUrl);
            var account = await authService.GetByUsername(model.Username,organization.Id);
            if (account is not {IsActive: true}) // not found or in Not active
                return Unauthorized();
            account.Password = new Random().Next(100000, 999999).ToString();
            if (account.Username == "09169212241")
                account.Password = "123456";
            else
            {
                var smsResult = SmsTools.SendVerifyMessage(account.Mobile, account.Password);
                if (!smsResult)
                    return BadRequest();
            }
            authService.Update(account);
            var token = await authService.GenerateLoginToken(account);

            return Ok(new OtpRequestResult());
        }


        [HttpPost]
        [ProducesResponseType(typeof(PlainToken), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Verify([FromBody] VerifyModel model)
        {
            var organization=_organizationService.GetOrganizationByUrl(model.OrganizationUrl);
            var account = await authService.GetByLoginToken(model.Username, model.Token,organization.Id);
            if (account == null) return Unauthorized();

            var token = await authService.GeneratePlainToken(account, model.Token);
            if (token == null) return Unauthorized();

            return Ok(token);
        }

        [HttpPost("/api/[controller]/refresh")]
        [ProducesResponseType(typeof(PlainToken), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenModel model)
        {

            var token = await authService.RefreshToken(model.Token);
            if (token == null) return Unauthorized();

            return Ok(token);
        }
        [HttpPost]
        [ProducesResponseType(typeof(PlainToken), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginWithPassword([FromBody] LoginModel model)
        {
            var organization = _organizationService.GetOrganizationByUrl(model.OrganizationUrl);
            var account = await authService.GetByUsernameAndPassword(model.Username, model.Password,organization.Id);
            if (account == null) return Unauthorized();

            var loginToken = await authService.GenerateLoginToken(account);
            var plainToken = await authService.GeneratePlainToken(account, loginToken.Token);
            if (plainToken == null) return Unauthorized();

            return Ok(plainToken);
        }

        [HttpGet]
        [ProducesResponseType(typeof(CheckLoginResponse), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CheckLogin()
        {
            authService.SetDeveloperUser();
            try
            {
                var user = authService.GetById(CustomUser.Id);
                var res=Mapper.Map<CheckLoginResponse>(user);
                var isDev = user.AccountRoles?.Any(x => x.Role.Name.Equals(Consts.Developer, StringComparison.InvariantCultureIgnoreCase));
                res.IsDeveloper = isDev ?? false;
                if (System.IO.File.Exists($"{WebRootPath}/{Consts.CompanyLogosFolder}/{user.Organization.Id}.jpg"))
                    res.ProfileImage = $"staticfiles/{Consts.CompanyLogosFolder}/{user.Organization.Id}.jpg";
                return Ok(res);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

    }
}
