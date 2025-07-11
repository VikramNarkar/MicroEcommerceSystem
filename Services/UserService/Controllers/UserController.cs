﻿using Microsoft.AspNetCore.Mvc;
using UserService.Repository;
using Common;
using UserService.Repository.Abstract;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepoUserService _repoUserService;

        public UserController(IRepoUserService repoUserService)
        {
            _repoUserService = repoUserService;            
        }

        [HttpGet]
        public IActionResult GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 5) 
        { 
            var users = _repoUserService.GetPagedUsers(page, pageSize);

            var total = _repoUserService.TotalCount;

            return Ok(new {
                Page = page,
                PageSize = pageSize,
                Total = total,
                Users = users
            });
        }
    }
}
