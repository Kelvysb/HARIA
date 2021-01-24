using System;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.DTOs;
using HARIA.Domain.Abstractions.Entities;
using HARIA.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace HARIA.API.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("/[controller]")]
    public abstract class ControllerBase<TEntity, TDTO> : Controller
        where TEntity : class, IEntity, new()
        where TDTO : class, IDTO, new()
    {
        protected IServiceBase<TEntity, TDTO> service;

        protected ControllerBase(IServiceBase<TEntity, TDTO> service)
        {
            this.service = service;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add(TDTO dto)
        {
            try
            {
                var result = await service.Add(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await service.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            try
            {
                var result = await service.Get();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await service.Get(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update(TDTO dto)
        {
            try
            {
                var result = await service.Update(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}