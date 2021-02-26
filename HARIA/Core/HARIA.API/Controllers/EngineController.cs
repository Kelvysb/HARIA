using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Constants;
using HARIA.Domain.DTOs;
using HARIA.Domain.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HARIA.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("/[controller]")]
    public class EngineController : Controller
    {
        private readonly IEngineService engineService;

        public EngineController(IEngineService engineService)
        {
            this.engineService = engineService;
        }

        [HttpPost]
        public virtual async Task<IActionResult> StateChange([FromBody] List<DeviceMessage> deviceMessages)
        {
            try
            {
                AuthHelper.CheckPermissions(User, Permissions.SERVICE);
                var result = await engineService.StateChange(deviceMessages);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{deviceCode}")]
        public virtual async Task<IActionResult> GetState([FromRoute] string deviceCode)
        {
            try
            {
                AuthHelper.CheckPermissions(User, Permissions.SERVICE);
                var result = await engineService.GetState(deviceCode);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}