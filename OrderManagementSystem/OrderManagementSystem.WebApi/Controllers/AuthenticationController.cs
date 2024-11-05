using System.Net;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.WebApi.Models;
using AuthenticationManager = OrderManagementSystem.WebApi.Managers.AuthenticationManager;

namespace OrderManagementSystem.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationManager _authenticationManager;
        public AuthenticationController(AuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(UserLoginModel user)
        {
            try
            {
                return await Task.FromResult<IActionResult>(Ok(new ServiceResponse<string>()
                {
                    Result = await _authenticationManager.GetTokenAsync(user)
                }));
            }
            catch (System.Security.Authentication.AuthenticationException e)
            {
                return await Task.FromResult<IActionResult>(Unauthorized(new ServiceResponse<string>()
                {
                    ErrorMessages = new List<string>(){e.Message}
                }));
            }
            catch (System.Exception e)
            {
                return await Task.FromResult<IActionResult>(BadRequest(new ServiceResponse<string>()
                {
                    ErrorMessages = new List<string>() { e.Message }
                }));
            }
        }

    }
}
