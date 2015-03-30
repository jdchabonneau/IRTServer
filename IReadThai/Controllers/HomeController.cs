using IReadThai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IReadThai.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int bookID = 2, int storyID = 1)
        {
            ThaiModels thaiModels = new ThaiModels();
            ViewBag.Title = "Home Page: ";
            //int size = thaiModels.dicSize();
          // string p = thaiModels. buildStory(bookID, storyID);
            var p = thaiModels.getParagraphInfo(2280);
            string t = thaiModels.test(p.sentenceInfos);
            t += p.translation + "<br>";
            t += p.soundUrl;
//            ViewBag.Text = p.ToString() + ": " + DateTime.Now.ToString();// p;
            ViewBag.Text = t;// p;
            return View();
        }

        public ActionResult Books()
        {
            ThaiModels thaiModels = new ThaiModels();
            return View(thaiModels.getBooks());
        }
    }
}

