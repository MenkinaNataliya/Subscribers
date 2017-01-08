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
        public ActionResult GetUserNews( string typeSort, int userId)
        {
            
            var news = AppServer.Server.GetNews(userId)
                .ConvertAll(new Converter<AppServer.News, Models.News>(ServerNewsToWebNews));
            ViewBag.news = news;
            ViewBag.id = userId;
            return View(Sorting(typeSort, news));
         }
        
        private static List<Models.News> Sorting(string typeSort, List<Models.News> news)
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
            else if (typeSort == "normalizationByLikes")
            {
                news.Sort(delegate (Models.News ne1, Models.News ne2)
                { return ne1.LikesPriority.CompareTo(ne2.LikesPriority); });
            }
            else if (typeSort == "normalizationByComments")
            {
                news.Sort(delegate (Models.News ne1, Models.News ne2)
                { return ne1.CommentsPriority.CompareTo(ne2.CommentsPriority); });
            }
            else if (typeSort == "normalizationByReposts")
            {
                news.Sort(delegate (Models.News ne1, Models.News ne2)
                { return ne1.RepostsPriority.CompareTo(ne2.RepostsPriority); });
            }
            return news;
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
                shares = news.share.share_count,
                LikesPriority= news.LikesPriority,
                CommentsPriority = news.CommentsPriority,
                RepostsPriority = news.RepostsPriority
            };

        }
    }
    
}
