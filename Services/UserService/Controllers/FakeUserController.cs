﻿using Microsoft.AspNetCore.Mvc;
using UserService.Repository;
using Common;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeUserController : ControllerBase
    {
        private readonly FakeUserService _userService;

        public FakeUserController(FakeUserService fakeUserService)
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
