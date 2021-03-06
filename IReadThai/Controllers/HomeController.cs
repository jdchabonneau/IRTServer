﻿using IReadThai.Models;
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
            var p = thaiModels.getParagraphInfo(2281);

            string t = thaiModels.test(p.sentenceInfos);
            t += "Translation: " + p.translation + " <br>";
            t += "SoundUrl: " +  p.soundUrl + " <br>";
//            ViewBag.Text = p.ToString() + ": " + DateTime.Now.ToString();// p;
            ViewBag.Text = t;// p;
            return View(p);
        }

        public ActionResult Books()
        {
            ThaiModels thaiModels = new ThaiModels();
            return View(thaiModels.getBooks());
        }
    }
}

