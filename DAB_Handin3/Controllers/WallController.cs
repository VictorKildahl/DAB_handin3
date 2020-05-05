using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB_Handin3.Models;
using DAB_Handin3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DAB_Handin3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallController : ControllerBase
    {
        private readonly WallService _wallService;

        public WallController(WallService wallService)
        {
            _wallService = wallService;
        }

        [HttpGet("{userName}/{guestName}")]
        public List<Post> Wall(string userName, string guestName)
        {
            return _wallService.Wall(userName, guestName);
        }
    }
}