using System;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Services;
using HARIA.Domain.Constants;
using HARIA.Domain.DTOs;
using HARIA.Domain.Entities;
using HARIA.Domain.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HARIA.API.Controllers
{
    public class UsersController : ControllerBase<UserEntity, User>
    {
        private IUsersService usersService;

        public UsersController(IUsersService service) : base(service)
        {
            usersService = service;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            try
            {
                var result = await usersService.Login(login);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(User login)
        {
            try
            {
                AuthHelper.CheckPermissions(User, PermissionConstants.DASHBOARD, PermissionConstants.CONFIGURE, PermissionConstants.KIOSK);
                var result = await usersService.ChangePassword(login);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}