using AnNet_GestContact.Dal.Services;
using AnNet_GestContact.Models;
using AnNet_GestContact.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnNet_GestContact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;

        public AuthController(UserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        [HttpPost]
        public IActionResult Login(LoginForm form)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                AppUser user = _userService.Login(form.Email, form.Password).ToApi();
                string token = _tokenService.GenerateJWT(user);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            
        }
    }
}
