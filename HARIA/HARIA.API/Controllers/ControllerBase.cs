using System;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Entities;
using HARIA.Domain.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HARIA.API.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("/[controller]")]
    public abstract class ControllerBase<TEntity> : Controller where TEntity : class, IEntity, new()
    {
        protected IServiceBase<TEntity> service;

        protected ControllerBase(IServiceBase<TEntity> service)
        {
            this.service = service;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add(TEntity entity)
        {
            try
            {
                var result = await service.Add(entity);
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
        public virtual async Task<IActionResult> Update(TEntity entity)
        {
            try
            {
                var result = await service.Update(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}