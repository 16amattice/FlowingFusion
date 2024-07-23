using FlowingFusion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowingFusion.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [Tags("User")]
    public class UserController : ControllerBase
    {
        private static readonly User CurrentUser = new User
        {
            Bio = "a sailor, a tailor",
            Name = "John Smith",
            TwitterHandle = null,
            UserId = "G_-mnBf9b1j9A7a4ub4nFQ==",
            Email = "johnsmith@gumroad.com",
            Url = "https://gumroad.com/sailorjohn"
        };

        /// <summary>
        /// Retrieves the user's data.
        /// </summary>
        /// <returns>The user's data.</returns>
        /// <response code="200">Returns the user's data.</response>
        [HttpGet]
        [ProducesResponseType(typeof(User), 200)]
        [Produces("application/json")]
        public IActionResult GetUser()
        {
            return Ok(new { success = true, user = CurrentUser });
        }
    }
}
