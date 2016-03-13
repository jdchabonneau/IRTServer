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
 [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ParagraphsController : ApiController
    {
        ThaiModels thaiModels = new ThaiModels();
        // GET: api/Paragraphs
        public ThaiModels.ParaInfo Get()
        {
            return null;
        }

        // GET: api/Paragraphs/5
        public ThaiModels.ParaInfo Get(int id)
        {
           return thaiModels.getParagraphInfo(id);
        }

        //[HttpGet]
        //[Route("GetParaSoundUrl")]
        //public string GetParaSoundUrl(int paraID)
        //{
        //    return thaiModels.paragraphSoundUrl(paraID);
        //}
             [HttpGet]
        public string Say()
        {
            return "Hello";
        }

             [HttpGet]
             public string Say(int id)
             {
                 return "Hello: " + id;
             }

             [HttpGet]
             public string Say(int a, int b)
             {
                 return "Hello: " + a*b;
             }

        //http://localhost:7696/api/paragraphs/GSU
        //[Route("GSU")]
        //public string GSU()
        //{
        //    return "gsu";
        //}

        //[Route("GSU")]
        //public string GSU(int? id)
        //{
        //    return "gsu: " + id;
        //}

        //[Route("GetStoryParagraphIDs")]
        //public int[] GetStoryParagraphIDs(string id)
        //{
        //    string[] tokens = id.Split(',');
        //    int bookID = int.Parse(tokens[0]);
        //    int storyID = int.Parse(tokens[1]);
        //    return thaiModels.getStoryParagraphIDs(bookID, storyID);
        //}

        //// POST: api/Paragraphs
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Paragraphs/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Paragraphs/5
        //public void Delete(int id)
        //{
        //}
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
