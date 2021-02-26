using System;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HARIA.API.Controllers
{
    public class ActuatorsController : ControllerBase<ActuatorEntity, Actuator>
    {
        private readonly IActuatorsService actuatorsService;

        public ActuatorsController(IActuatorsService service) : base(service)
        {
            actuatorsService = service;
        }

        [HttpGet("ByCode/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            try
            {
                var result = await actuatorsService.GetByCode(code);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ByDevice/{deviceId}")]
        public async Task<IActionResult> GetByDevice([FromRoute] int deviceId)
        {
            try
            {
                var result = await actuatorsService.GetByDevice(deviceId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}