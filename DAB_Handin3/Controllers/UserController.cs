using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB_Handin3.Models;
using DAB_Handin3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace DAB_Handin3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public ActionResult<List<User>> Get() =>
            _userService.Get();

        // GET: api/User/UserName
        [HttpGet("{userName}", Name = "GetUser")]
        public ActionResult<User> Get(string userName)
        {
            var user = _userService.Get(userName);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/User
        [HttpPost]
        public ActionResult<User> Create([FromBody] User user)
        {
            _userService.Create(user);
            return CreatedAtRoute("GetUser", new { userName = user.UserName }, user);
        }

        // PUT: api/User/UserName
        [HttpPut("{userName}")]
        public IActionResult Update(string userName, [FromBody] User userIn)
        {
            var user = _userService.Get(userName);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Update(userName, userIn);
            return NoContent();
        }

        // DELETE: api/User/UserName
        [HttpDelete("{userName}")]
        public IActionResult Delete(string userName)
        {
            var user = _userService.Get(userName);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Remove(user.UserName);

            return NoContent();
        }


        // POST: /api/user/follow/UserName/FollowName
        [HttpPost("follow/{userName}/{follow}")]
        public void follow_user(string userName, string follow)
        {
            _userService.follow_user(userName, follow);
        }

        // POST: /api/user/block/UserName/BlockName
        [HttpPost("block/{userName}/{block}")]
        public void block_user(string userName, string block)
        {
            _userService.block_user(userName, block);
        }

        // POST: /api/user/login/UserName/password
        [HttpPost("login/{userName}/{password}")]
        public void Login(string userName, string password)
        {
            _userService.Login(userName, password);
        }

    }
}