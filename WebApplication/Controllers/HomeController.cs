using System.Web.Mvc;
using AppServer;
using System;
using System.Collections.Generic;
using System.Linq;


namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View(AppServer.Server.GetMembers("csu_iit")
                .ConvertAll(new Converter<AppServer.User, Models.User>(ServerUserToWebUser)));
        }

        [HttpGet]
        public ActionResult GetUserNews(int userId, string type = "likes")
        {
            
            var news = AppServer.Server.GetNews(userId)
                .ConvertAll(new Converter<AppServer.News, Models.News>(ServerNewsToWebNews));
            ViewBag.news = news;
            ViewBag.id = userId;
            return View( news);
         }
                   
        [HttpGet]
        public ActionResult Sort(string typeSort , int id)
        {
            var news = AppServer.Server.GetNews(id)
                .ConvertAll(new Converter<AppServer.News, Models.News>(ServerNewsToWebNews));
            
            return View(Sorting(typeSort, news));
        }
        public static List<Models.News> Sorting(string typeSort, List<Models.News> news)
        {
            if (typeSort == "comments")
            {
                news.Sort(delegate (Models.News ne1, Models.News ne2)
                { return ne2.comments.CompareTo(ne1.comments); });
            }

            else if (typeSort == "reposts")
            {
                
                news.Sort(delegate (Models.News ne1, Models.News ne2)
                { return ne2.reposts.CompareTo(ne1.reposts); });
            }
            else if (typeSort == "likes")
            {
                news.Sort(delegate (Models.News ne1, Models.News ne2)
                { return ne2.likes.CompareTo(ne1.likes); });
            }
            else if (typeSort == "shares")
            {
                news.Sort(delegate (Models.News ne1, Models.News ne2)
                { return ne2.shares.CompareTo(ne1.shares); });
            }
            return news;
        }


        [HttpGet]
        public ActionResult News(List<Models.News> news)
        {
            if(news ==null) return RedirectToAction("Error");
            return View(news);
        }
        [HttpGet]
        public ActionResult Error()
        {
           
            return View();
        }

        private Models.User ServerUserToWebUser(AppServer.User user)
        {
            return new Models.User
            {
                Uid = user.Uid,
                FirstName = user.FirstName,
                Deactivated = user.Deactivated,
                SecondName = user.SecondName,
                Photo = user.Photo
            };
        }

        private Models.News ServerNewsToWebNews(AppServer.News news)
        {
            return new Models.News
            {
                likes = news.likes.count,
                comments = news.comments.count,
                reposts = news.reposts.count,
                text = news.text,
                photo = news.photo,
                shares = news.share.share_count
             };

        }
    }
    
}
