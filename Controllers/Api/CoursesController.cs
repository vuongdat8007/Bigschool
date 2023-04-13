using Bigschool_TH_11.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bigschool_TH_11.Controllers.Api
{
    public class CoursesController : ApiController
    {
        public ApplicationDbContext _context { get; set; }
        public CoursesController() 
        { 
            _context = new ApplicationDbContext();
        }

        /*[HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _context.Courses.Single(c => c.Id == id && c.LecturerId == userId);
            if(course.IsCancelled)
                return NotFound();
            course.IsCancelled = true;
            _context.SaveChanges();

            return Ok();
        }*/
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _context.Courses.Single(c => c.Id == id && c.LecturerId == userId);
            if(course.IsCancelled)
                return NotFound();
            course.IsCancelled = true;
            _context.SaveChanges();

            return Ok();
        }
    }
}