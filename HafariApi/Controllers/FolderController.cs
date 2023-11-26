using AutoMapper;
using Domain.BaseData;
using Domain.Config;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.BaseData;
using Models.Identity;
using Services.BaseData;
using Services.Configs;
using Services.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class FolderController : BaseController
    {
        private readonly IAuthService _authorizationService;
        private readonly IFolderService _folderService;
        public FolderController(IMapper mapper, IWebHostEnvironment webHostEnvironment,
            IAuthService authorizationService, IFolderService folderService)
            : base(mapper, webHostEnvironment)
        {
            _authorizationService = authorizationService;
            _folderService = folderService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Save(FolderModel model)
        {
            var folder = new Folder();
            if (model.Id.HasValue)
            {
                folder=_folderService.GetById(model.Id.Value);
                if (folder.AccountId != CustomUser.Id && !folder.IsPublic)
                    return Forbid();
                folder = _folderService.GetById(model.Id.Value);
            }
            Mapper.Map(model, folder);
            folder.AccountId = CustomUser.Id;
            _folderService.AddOrUpdate(folder);
            return Ok();
        }
        
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<FolderModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetAll()
        {
            return Ok(Mapper.Map<List<FolderModel>>(_folderService.GetByUserIdAndPublicFolders(CustomUser.Id)));
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Remove(Guid folderId)
        {
            var folder = _folderService.GetById(folderId);
            if (folder.AccountId != CustomUser.Id)
                return Forbid();
            _folderService.Delete(folder);
            return Ok();
        }
    }
}
