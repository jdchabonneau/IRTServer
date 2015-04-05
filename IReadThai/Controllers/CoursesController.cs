using IReadThai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IReadThai.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CoursesController : ApiController
    {
        ThaiModels thaiModels = new ThaiModels();
        // GET: api/Courses
        public IEnumerable<Course> Get()
        {
            return thaiModels.getCourses();
        }

        // GET: api/Courses/5
        public IEnumerable<CourseChapter> Get(int id)
        {
            return thaiModels.getCourseChapters(id);
        }

        [HttpGet]
        public IReadThai.Models.ThaiModels.StoryData getStoryData(int bookID, int storyID)
        {
            return thaiModels.getStoryData(bookID, storyID);
        }

        // POST: api/Courses
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Courses/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Courses/5
        public void Delete(int id)
        {
        }
    }
}
