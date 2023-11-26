using AutoMapper;
using Domain.BaseData;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.BaseData;
using Models.Identity;
using Services.BaseData;
using Services.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class OrganizationController : BaseController
    {
        private readonly IAuthService _authorizationService;
        private readonly IOrganizationService _organizanitionService;
        private readonly IOrganizationAdminService _organizationAdminService;
        public OrganizationController(IMapper mapper, IWebHostEnvironment webHostEnvironment,
            IOrganizationService organizanitionService, IOrganizationAdminService organizationAdminService,
            IAuthService authorizationService)
            : base(mapper, webHostEnvironment)
        {
            _organizanitionService = organizanitionService;
            _organizationAdminService = organizationAdminService;
            _authorizationService = authorizationService;
        }

        [Authorize(Roles = Consts.Developer)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Save(OrganizationModel model)
        {
            var organiztion = new Organization();
            if (model.Id.HasValue)
                organiztion = _organizanitionService.GetById(model.Id.Value);
            Mapper.Map(model, organiztion);
            _organizanitionService.AddOrUpdate(organiztion);
            var allAdmins = _organizationAdminService.GetAdminsByOrganizationId(organiztion.Id);          
            _organizanitionService.SaveChanges();           
            UploadLogo(model.Logo, organiztion.Id);
            return Ok();
        }

        private bool UploadLogo(UploadedFile file, Guid organizationId)
        {
            try
            {

                if (file == null || string.IsNullOrWhiteSpace(file.FileName) || file.FileContent.Length == 0 || file.FileContent.Length>1000000) return false;
                System.IO.Directory.CreateDirectory($"{WebRootPath}/{Consts.CompanyLogosFolder}" );                
                var fileName = WebRootPath + $"/{Consts.CompanyLogosFolder}/{organizationId}.jpg";
                var fs = System.IO.File.Create(fileName);
                fs.Write(file.FileContent, 0, file.FileContent.Length);
                fs.Close();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        [Authorize(Roles = Consts.Developer)]
        [HttpGet]
        [ProducesResponseType(typeof(OrganizationModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetById(Guid organizationId)
        {
            var organiztion = _organizanitionService.GetById(organizationId);
            var model = Mapper.Map<OrganizationModel>(organiztion);
            model.Admins = Mapper.Map<List<AccountModel>>(_organizationAdminService.GetAdminsByOrganizationId(organizationId).Select(x => x.Account));
            return Ok(model);
        }

        [Authorize(Roles = Consts.Developer)]
        [HttpGet]
        [ProducesResponseType(typeof(List<OrganizationModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetAll()
        {
            var organiztions = _organizanitionService.GetQueryable().ToList();
            return Ok(Mapper.Map<List<OrganizationModel>>(organiztions));
        }

        [Authorize(Roles = Consts.Developer)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult SaveLogo(Guid organizationId)
        {
            var org = _organizanitionService.GetById(organizationId);
            var file = Request.Form.Files[0];
            System.IO.Directory.CreateDirectory(WebRootPath + "/CompanyLogos");            
            var fileName = WebRootPath + $"/CompanyLogos/{organizationId}.jpg";
            using (var output = System.IO.File.Create(fileName))
            {
                file.CopyTo(output);
            }
            return Ok();
        }

        [Authorize(Roles = Consts.Developer)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Remove(Guid organizationId)
        {
            var org = _organizanitionService.GetById(organizationId);
            _organizanitionService.Delete(org);
            return Ok();
        }
    }
}
