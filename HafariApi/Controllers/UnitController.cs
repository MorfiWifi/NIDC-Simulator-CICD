using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Config;
using Services.Configs;

namespace Api.Controllers
{
    public class UnitController : BaseController
    {
        private readonly IUnitService _unitService;

        public UnitController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IUnitService unitService)
            : base(mapper, webHostEnvironment)
        {
            _unitService = unitService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult Save(List<UnitModel> models)
        {
            var domainModels = Mapper.Map<List<Unit>>(models);
            _unitService.UpdateUserUnits(domainModels, CustomUser.Id);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<UnitModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetAll()
        {
            return Ok(Mapper.Map<List<UnitModel>>(_unitService.GetAllUnits()));
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<UnitModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Get()
        {
            return Ok(Mapper.Map<List<UnitModel>>(_unitService.GetUserUnits(CustomUser.Id)));
        }
    }
}