using AutoMapper;
using Domain.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Config;
using Newtonsoft.Json;
using Services.Configs;
using System;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Authorize]
    public partial class ConfigController:BaseController
    {
        private readonly IConfigService _configService;
        private readonly IUnitService _unitService;

        public ConfigController(IMapper mapper,IConfigService configService , IUnitService unitService) : base(mapper)
        {
            _configService = configService;
            _unitService = unitService;
        }       
        [HttpPost]
        [ProducesResponseType(typeof(ConfigModel), StatusCodes.Status200OK)]

        public IActionResult AddNewConfig(ConfigModel model)
        {
            var config=Mapper.Map<SimulationConfig>(model);
            config.AccountId=CustomUser.Id;
            _configService.Add(config);
            return Ok(Mapper.Map<ConfigModel>(config));
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]


      
        [HttpGet]
        [ProducesResponseType(typeof(List<ConfigModel>), StatusCodes.Status200OK)]        


        public IActionResult GetUserConfigs()
        {
            var configs=_configService.GetUserConfigs(CustomUser.Id , CustomUser.Roles);
            return Ok(Mapper.Map<List<ConfigModel>>(configs));
        }
        [HttpGet]
        [ProducesResponseType(typeof(ConfigDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]

        public IActionResult GetConfigDetails(Guid configId)
        {
            var config = _configService.GetById(configId);
            if(config.AccountId!=CustomUser.Id)
                return Unauthorized(new ResponseModel { Message = "Access Error!" });
            return Ok(String.IsNullOrEmpty(config.ConfigDetails) ? 
                new ConfigDetails():JsonConvert.DeserializeObject<ConfigDetails>(config.ConfigDetails));
        }
        [HttpPost]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]

        public IActionResult SaveConfigDetails([FromQuery]Guid configId, ConfigDetails model)
        {
            var config = _configService.GetById(configId);
            if (config.AccountId != CustomUser.Id)
                return Unauthorized(new ResponseModel { Message = "Access Error!" });
            config.ConfigDetails=JsonConvert.SerializeObject(model);
            _configService.Update(config);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]

        public IActionResult DeleteConfig([FromQuery] Guid configId)
        {
            var config = _configService.GetById(configId);
            if (config.AccountId != CustomUser.Id)
                return Unauthorized(new ResponseModel { Message = "Access Error!" });           
            _configService.Delete(config);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]

        public IActionResult CopyConfig([FromQuery] Guid configId)
        {
            var config = _configService.GetById(configId);
            if(!config.Folder.IsPublic && config.AccountId!=CustomUser.Id)
                return Unauthorized(new ResponseModel { Message = "Access Error!" });
            var newConfig = new SimulationConfig();
            Mapper.Map(config, newConfig);
            newConfig.Title += (new Random()).Next(minValue: 1000, maxValue:9999);
            newConfig.Account = null;
            newConfig.AccountId = CustomUser.Id;
            newConfig.Id = Guid.NewGuid();
            _configService.Add(newConfig);
            return Ok();
        }
    }
}
