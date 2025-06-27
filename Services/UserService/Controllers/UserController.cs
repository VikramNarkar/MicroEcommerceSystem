using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FakeUserService _userService;

        public UserController(FakeUserService fakeUserService)
        {
            _userService = fakeUserService;            
        }

        [HttpGet]
        public IActionResult GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 5) 
        { 
            var users = _userService.GetPagedUsers(page, pageSize);

            var total = _userService.TotalCount;

            return Ok(new {
                Page = page,
                PageSize = pageSize,
                Total = total,
                Users = users
            });
        }
    }
}
