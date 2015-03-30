using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace IReadThai.Models
{
    public class ThaiModels
    {
        int ss = 0;
        ThaiSQLEntities ctx = new ThaiSQLEntities();
        Word[] wordsArray = null;
        public string buildSentence(int sentenceID)
        {
            if (wordsArray == null)
            {
                wordsArray = ctx.Words.ToArray();
            }

            var wordIDs = ctx.Sentences.Where(s => s.ID == sentenceID).OrderBy(s => s.Sequence).Select(w => w.WordID).ToArray();
            string st = ss + ":";
            ss++;
//            var wordsArray = ctx.Words.Where(w => wordIDs.Contains(w.ID)).ToArray();
            foreach (int wordID in wordIDs)
            {
                st += wordsArray.First(w => w.ID == wordID).Word1;
            }
            return st;
        }

        public string buildParagraph(int paragraphID)
        {
            var SentenceIDs = ctx.Paragraphs.Where(s => s.ID == paragraphID).OrderBy(s => s.Sequence).Select(w => w.SentenceID).ToArray();
            string st = "";
            foreach (int SentenceID in SentenceIDs)
            {
                st += buildSentence(SentenceID) + " ";
            }
            return st;
        }

        public string buildStory(int bookID, int storyID)
        {
            var paraIDs = ctx.Stories.Where(s => s.BookID == bookID && s.ID == storyID).OrderBy(s => s.Sequence).Select(w => w.ParagraphID).ToArray();
            string st = "";
            foreach (int paraID in paraIDs)
            {
                st += buildParagraph(paraID) + System.Environment.NewLine + System.Environment.NewLine;
            }
            return st;
        }

        public int dicSize()
        {
            var lll = ctx.Words.ToArray();
            int size = 0;
            foreach (Word word in lll)
            {
                size += word.Word1.Length * 3;
            }
            int IDs = lll.Count() * 4;
            return size;
        }

        public Book[] getBooks()
        {
            return ctx.Books.OrderBy(b => b.Title).ToArray();
        }

        public Course[] getCourses()
        {
            return ctx.Courses.ToArray();
        }

        public CourseChapter[] getCourseChapters(int courseID)
        {
            return ctx.CourseChapters.Where(c=>c.CourseID == courseID).OrderBy(c=>c.ChapterID).ToArray();
        }

        public IEnumerable<ViewWord> getWords()
        {
            var words = ctx.ViewWords.ToArray();
            return words;
        }

        //int[] getParagraphIDs(int bookID, int chapterID)
        //{
            
        //}

        public string paragraphSoundUrl(int paragraphID)
        {
            ////http://citiesarecool.com/data/Book%20Jack1/waan.txt/waan_1_10_1.mp3
            //string server = "http://citiesarecool.com/data";
            //string speaker = "waan";
            //int bookID, chapterIndex, paragraphIndex;
            //string bookLocation;
            //getParagraphContext(paragraphID, out bookLocation, out bookID, out chapterIndex, out paragraphIndex);
            //return string.Format("{0}/{1}/{2}_{3}_{4}.mp3", server, bookLocation, speaker, bookID, chapterIndex, paragraphIndex);
            var url = ctx.ParagraphSoundUrls.SingleOrDefault(p => p.ParaID == paragraphID);
            return (url == null) ? "" : url.Url;
        }

        public string sentenceTranslation(int sentenceID)
        {
            var translation = ctx.SentenceTranslationEnglishes.SingleOrDefault(s=>s.SentenceID==sentenceID);
            return translation == null ? "" : translation.Translation;
        }

        public string paragraphTranslation(int paragraphID)
        {
            var translation = ctx.ParagraphTranslationEnglishes.SingleOrDefault(p => p.ParagraphID == paragraphID);
            return translation == null ? "" : translation.Translation;
        }

        public int[] paragraphTimings(int paragraphID)
        {
            var timings = ctx.Timings.Where(t => t.ParagraphID == paragraphID).OrderBy(t => t.Timing1).Select(t => t.Milliseconds).ToArray();
            return timings;
        }

        SentenceInfo[] paragraphSentenceInfo(int paragraphID)
        {
            List<SentenceInfo> sentenceInfos = new List<SentenceInfo>();
            foreach (var sentence in ctx.Paragraphs.Where(p => p.ID == paragraphID).OrderBy(p => p.Sequence))
            {
                SentenceInfo sentenceInfo = new SentenceInfo();
                sentenceInfos.Add(sentenceInfo);
                sentenceInfo.translation = sentenceTranslation(sentence.SentenceID??0);
                sentenceInfo.wordIDs = sentenceWordIDs(sentence.SentenceID ?? 0);
            }
            return sentenceInfos.ToArray();
        }

        private int[] sentenceWordIDs(int sentenceID)
        {
            return ctx.Sentences.Where(s => s.ID == sentenceID).OrderBy(s => s.Sequence).Select(s => s.WordID).ToArray();
        }

        public ParaInfo getParagraphInfo(int paragraphID)
        {
            ParaInfo paraInfo = new ParaInfo();
            paraInfo.soundUrl = paragraphSoundUrl(paragraphID);
            paraInfo.translation = paragraphTranslation(paragraphID);
            paraInfo.timings = paragraphTimings(paragraphID);
            paraInfo.sentenceInfos = paragraphSentenceInfo(paragraphID);
            return paraInfo;
        }

        public string test(SentenceInfo[] ss)
        {
            var words = getWords();
            StringBuilder sb = new StringBuilder();
            foreach (SentenceInfo sentence in ss)
            {
                foreach(int wordID in sentence.wordIDs)
                sb.Append(words.First(w=>w.ID==wordID).Word);
            }
            sb.Append("   ");
            return sb.ToString();
        }

        public int[] getStoryParagraphIDs(int bookID, int storyID)
        {
            return ctx.Stories.Where(s => s.BookID == bookID && s.ID == storyID).OrderBy(s => s.Sequence).Select(s => s.ParagraphID).ToArray();
        }

        public class SentenceInfo
        {
            public int[] wordIDs;
            public string translation;
        }
        public class ParaInfo
        {
            public SentenceInfo[] sentenceInfos;
            public string translation;
            public string soundUrl;
            public int[] timings;
        }

        private void getParagraphContext(int paragraphID, out string bookLocation, out int bookID, out int chapterIndex, out int paragraphIndex)
        {
            var para = ctx.Paragraphs.Single(p => p.ID == paragraphID);
            paragraphIndex = para.Sequence;
            Story story = ctx.Stories.First(s => s.ParagraphID == paragraphID);
            bookID = story.BookID;
            int bID = bookID;
            chapterIndex = story.ID;
            bookLocation = ctx.Books.Single(b => b.ID == bID).RootDirectory;
        }

        void makeurls()
        {
            HashSet<int> h = new HashSet<int>();
            Dictionary<int, string> l = new Dictionary<int, string>();
            //http://citiesarecool.com/data/Book%20Jack1/waan.txt/waan_1_10_1.mp3
            string server = "http://citiesarecool.com";
            string speaker = "waan";
            foreach (var book in ctx.Books)
            {
                console_WriteLine("New Book: " + book.Title);
                foreach (int storyID in ctx.Stories.Where(s => s.BookID == book.ID && s.Sequence == 1).Select(s => s.ID))
                {
                    console_WriteLine("new storyID: " + storyID);
                    //foreach(int sentenceID in ctx.Sentences.Where(s=>s.s))
                    foreach (Story story in ctx.Stories.Where(s => s.ID == storyID && s.BookID == book.ID))
                    {
                        //console_WriteLine("New paraID: " + story.ParagraphID);
                        string url = string.Format("{0}/{1}{2}_{3}_{4}_{5}.mp3", server, book.RootDirectory.Replace('\\','/'), speaker, book.ID, storyID, story.Sequence+1);
                        ParagraphSoundUrl psu = new ParagraphSoundUrl();
                        psu.ParaID = story.ParagraphID;
                        psu.Url = url;
                        ctx.ParagraphSoundUrls.Add(psu);
//                        l.Add(story.ParagraphID, url);
                        if (h.Contains(story.ParagraphID))
                        {
                            DateTime dt = DateTime.Now;
                        }
                        h.Add(story.ParagraphID);
                    }
                }
            }
            int jdc = ctx.SaveChanges();
            jdc++;
        }

        List<string> l = new List<string>();
        private void console_WriteLine(string p)
        {
            l.Add(p);
        }

    }

}