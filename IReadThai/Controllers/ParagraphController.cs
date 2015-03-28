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
//    [RoutePrefix("api/Paragraphs")]
//    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ParagraphsController : ApiController
    {
        ThaiModels thaiModels = new ThaiModels();
        // GET: api/Paragraphs
        public string Get()
        {
            return"xxx";
        }

        // GET: api/Paragraphs/5
        public ThaiModels.ParaInfo Get(int id)
        {
           return thaiModels.getParagraphInfo(id);
        }

        // POST: api/Paragraphs
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Paragraphs/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Paragraphs/5
        public void Delete(int id)
        {
        }
    }

    public class TotalParagraph
    {
        public string soundURL;
        public Sentence[] sentences;
        public int[] milliSeconds;
        public string translation;
    }

    public class Sentence
    {
        public int[] wordIDs;
        string translation;
    }
}
