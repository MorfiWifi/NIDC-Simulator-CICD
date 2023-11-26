using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Config;
using Newtonsoft.Json;

namespace Api.Controllers
{
    public partial class ConfigController
    {
        [HttpGet]
        [ProducesResponseType(typeof(ConfigDetails), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]

        public IActionResult GetConfigDetailsAndUnits(Guid configId)
        {
            var config = _configService.GetById(configId);
            if(config.AccountId!=CustomUser.Id)
                return Unauthorized(new ResponseModel { Message = "Access Error!" });
            var units = _unitService.GetUserUnits(CustomUser.Id);
            var unitModels = Mapper.Map<List<UnitModel>>(units);

            var configDetail =
                String.IsNullOrEmpty(config.ConfigDetails)
                    ? new ConfigDetails()
                    : JsonConvert.DeserializeObject<ConfigDetails>(config.ConfigDetails);

            if (configDetail != null)
            {
                configDetail.Units = unitModels;
            }
            
            return Ok(configDetail);
        }
        
    }
}