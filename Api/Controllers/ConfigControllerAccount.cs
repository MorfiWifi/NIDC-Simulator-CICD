using System;
using System.Collections.Generic;
using Domain.Config;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Config;
using Newtonsoft.Json;

namespace Api.Controllers
{
    public partial class ConfigController
    {
        [Authorize(Roles = Consts.AdminOrDeveloper )]
        [HttpGet]
        [ProducesResponseType(typeof(List<AccountConfigModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]

        public IActionResult GetConfigAccounts(Guid configId)
        {
            var accountConfigs = _configService.GetConfigsAccounts(configId);
            var accountConfigModels = Mapper.Map<List<AccountConfigModel>>(accountConfigs);

            return Ok(accountConfigModels);
        }
        
        [Authorize(Roles = Consts.AdminOrDeveloper)]
        [HttpPost]
        [ProducesResponseType(typeof(List<AccountConfigModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseModel), StatusCodes.Status401Unauthorized)]

        public IActionResult SaveAccountConfigs(List<AccountConfigModel> accountConfigModels)
        {
            var accountConfigs = Mapper.Map<List<AccountConfig>>(accountConfigModels);
            _configService.AddOrUpdateConfigsAccounts(accountConfigs);

            var configId = accountConfigs[0].ConfigId;
            return GetConfigAccounts(configId!.Value);
        }
    }
}