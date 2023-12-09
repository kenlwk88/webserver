using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Application;
using Web.Domain;
using Web.Domain.User;
using Web.Domain.User.Common;
using Web.Server.Filter.Security;
using Web.Server.Filter.Validation;

namespace Web.Server.Controllers
{
    [ApiKey]
    [ApiController]
    [ValidationFilter]
    [Route("api/user")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommonResponse), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(ValidationErrorResponse), (int)HttpStatusCode.BadRequest)]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserServices _services;
        public UserController(ILogger<UserController> logger, IUserServices services)
        {
            _logger = logger;
            _services = services;
        }
        /// <summary>
        /// Get User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(GetUserApiResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] ApplyFilter filters)
        {
            var result = await _services.Get(filters);
            return Ok(result);
        }
        /// <summary>
        /// Register User
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(RegisterUserApiResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Register([FromBody] RegisterUserApiRequest request)
        {
            var result = await _services.Register(request);
            return Ok(result);
        }
        /// <summary>
        /// Update User
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(UpdateUserApiResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] UpdateUserApiRequest request)
        {
            var result = await _services.Update(request);
            return Ok(result);
        }
        /// <summary>
        /// Delete User
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(typeof(DeleteUserApiResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromBody] DeleteUserApiRequest request)
        {
            var result = await _services.Delete(request);
            return Ok(result);
        }
    }
}
