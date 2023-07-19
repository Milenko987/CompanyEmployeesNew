using CompanyEmployeesNew.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [Route("api/authentication")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribite))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDTO userForRegistrationDTO)
        {
            var result = await _service.AuthService.RegisterUser(userForRegistrationDTO);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribite))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthDTO userForAuthDTO)
        {
            if (!await _service.AuthService.ValidateUser(userForAuthDTO))
            {
                return Unauthorized();
            }
            return Ok(new { Token = await _service.AuthService.CreateToken() });
        }

    }
}
