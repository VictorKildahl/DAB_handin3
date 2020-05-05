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
    public class FeedController : ControllerBase
    {
        private readonly FeedService _feedService;

        public FeedController(FeedService feedService)
        {
            _feedService = feedService;
        }


        // GET: api/Feed/logged_in_userName
        [HttpGet("{logged_in_userName}")]
        public ActionResult<List<Post>> Feed(string logged_in_userName)
        {
            return _feedService.Feed(logged_in_userName);
        }





    }
}