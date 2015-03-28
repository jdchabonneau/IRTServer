using IReadThai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IReadThai.Controllers
{
    public class WordsController : ApiController
    {
        // GET: api/Courses
        public IEnumerable<ViewWord> Get()
        {
            ThaiModels thaiModels = new ThaiModels();
            return thaiModels.getWords();
        }

        // GET: api/Courses/5
        public string Get(int id)
        {
            return "id = " +id;
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
