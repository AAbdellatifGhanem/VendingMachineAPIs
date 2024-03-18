using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VendingMachineAPIs.BLL.DTOs;
using VendingMachineAPIs.BLL.DTOs.UserDTOs;
using VendingMachineAPIs.BLL.Services.AuthService;
using VendingMachineAPIs.BLL.Services.UserService;
using VendingMachineAPIs.DAL.Models;

namespace VendingMachineAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IToken _authService;
        public UserController(IUserService userService,IToken authService)
        {
            _authService = authService;
            _userService = userService;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserDTO newUser)
        {
            if (newUser == null)
                return BadRequest();
            var created = await _userService.Register(newUser);
            return StatusCode(created.Status, created.Message);
        }


        [HttpPost]
        [Route("GetUser")]
        public async Task<ActionResult<GetUserResponseDTO>> Login(LoginDTOModel loginDto)
        {
            var result = await _authService.GenerateLoginResponseWithToken(loginDto);
            return result == null ?
                   NotFound("User with this credentials not found") :
                   Ok(result);
        }


        [Authorize]
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult> Update(UpdateUserDTO _updatedUser)
        {
            var token = _authService.ExtractToken(Request);
            var result =await _userService.UpdateUser(_updatedUser,_authService.GetUserIdFromToken(token));
            return StatusCode(result.Status, result.Message);
        }


        [Authorize]
        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<ActionResult> Delete()
        {
            var token = _authService.ExtractToken(Request);
            var result = await _userService.DeleteUser(_authService.GetUserIdFromToken(token));
            return StatusCode(result.Status, result.Message);
        }


        [Authorize]
        [HttpPut]
        [Route("UpdatePassword")]
        public async Task<ActionResult> UpdatePassword(ChangePasswordDTO passDTO)
        {
            var token = _authService.ExtractToken(Request);
            var result = await _userService.ChangePassword(passDTO, _authService.GetUserIdFromToken(token));
            return StatusCode(result.Status, result.Message);
        }

       




    }
}
