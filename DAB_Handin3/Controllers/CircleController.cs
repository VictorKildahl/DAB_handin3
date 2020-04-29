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
    public class CircleController : ControllerBase
    {
        private readonly CircleService _circleService;

        public CircleController(CircleService circleService)
        {
            _circleService = circleService;
        }


        // GET: api/Circle
        [HttpGet]
        public ActionResult<List<Circle>> Get() =>
            _circleService.Get();

        [HttpGet("{groupName}", Name = "GetCircle")]
        public ActionResult<List<Circle>> Get(string groupName)
        {
            var circle = _circleService.Get(groupName);
            if (circle == null)
            {
                return NotFound();
            }

            return circle;
        }

        // POST: api/Circle
        [HttpPost]
        public ActionResult<Circle> Create(Circle circle)
        {
            _circleService.Create(circle);
            return CreatedAtRoute("GetCircle", new { groupName = circle.CircleName }, circle);
        }

        // PUT: api/Circle/id
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] User user)
        {
            var circle = _circleService.Get(id);

            if (circle == null)
            {
                return NotFound();
            }

            _circleService.Update(id, user);
            //return NoContent();
            return Ok();
        }

        // DELETE: api/Circle/id
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var circle = _circleService.Get(id);

            if (circle == null)
            {
                return NotFound();
            }

            _circleService.Remove(id);
            return NoContent();
        }
    }
}