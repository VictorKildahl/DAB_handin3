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
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        // GET: api/Post
        [HttpGet]
        public ActionResult<List<Post>> Get()
        {
            return _postService.Get();
        }

        // GET: api/Post/Author
        [HttpGet("{author}", Name = "GetPost")]
        public ActionResult<List<Post>> Get(string author)
        {
            var user = _postService.Get(author);
            if (user == null)
                return NotFound();

            return user;
        }

        // POST: api/Post
        [HttpPost]
        public ActionResult<Post> Post([FromBody] Post post)
        {
            //post.Time = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
            post.Time = DateTime.Now;
            _postService.CreatePost(post);
            return CreatedAtAction("Post", new { id = post.Id, post });
        }

        // PUT: api/Post/5
        [HttpPut("{text}")]
        public IActionResult PutComment(string text, [FromBody] Comment comment)
        {
            var user = _postService.Get(text);
            if (user == null)
                return NotFound();

            _postService.Update(text, comment);
            return Ok();
        }

        // DELETE: api/Post/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var user = _postService.Get(id);
            if (user == null)
                return NotFound();

            _postService.Remove(id);
            return Ok();
        }
    }
}


