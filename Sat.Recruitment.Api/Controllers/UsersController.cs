using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Services;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : BaseController<UsersController>
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService, ILogger<UsersController> logger) : base(logger)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetTest()
        {
            try
            {
                var response = "response";
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal Server error", ex);
                return StatusCode(500, string.Format("Internal server error\n{0}", GetExceptionInsights(ex)));
            }
        }

        [HttpPost]
        public ActionResult CreateUser([FromBody]User user)
        {
            try
            {
                Result result = _userService.CreateUser(user);
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal Server Error", ex);
                return StatusCode(500, string.Format("Internal server error\n{0}", GetExceptionInsights(ex)));
            }
        }

    }
}
