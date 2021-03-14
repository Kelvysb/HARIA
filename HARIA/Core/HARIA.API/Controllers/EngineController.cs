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
        public virtual async Task<IActionResult> StateChange([FromBody] List<NodeMessage> nodeMessages)
        {
            try
            {
                AuthHelper.CheckPermissions(User, Permissions.SERVICE);
                var result = await engineService.StateChange(nodeMessages);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{nodeCode}")]
        public virtual async Task<IActionResult> GetState([FromRoute] string nodeCode)
        {
            try
            {
                AuthHelper.CheckPermissions(User, Permissions.SERVICE);
                var result = await engineService.GetState(nodeCode);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("checkScript")]
        public virtual async Task<IActionResult> CheckScript([FromBody] string script)
        {
            try
            {
                AuthHelper.CheckPermissions(User, Permissions.CONFIGURE);
                var result = await engineService.CheckScript(script);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}