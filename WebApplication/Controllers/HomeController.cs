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
            if (news.Count == 0) ViewBag.Message = "Пользователь не имеет записей на стене, либо закрыл доступ из вне";
            else ViewBag.Message = "Записи со стены " + AppServer.Server.GetNameById(userId);
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
            else if (typeSort == "normalization")
            {
                var coefficient = AppServer.Server.CalculateCoefficient(news.ConvertAll(new Converter<Models.News, News>(ModelsNewsToNews)));
                news.Sort(delegate (Models.News ne1, Models.News ne2)
                { return ne2.GetPrioritet(coefficient).CompareTo(ne1.GetPrioritet(coefficient)); });
            }
           
            return news;
        }

        private static News ModelsNewsToNews(Models.News news)
        {
             return new News
                {
                    likes = new Likes { count = news.likes },
                    comments = new Comments { count = news.comments },
                    reposts = new Reposts { count = news.reposts }
                };
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
                attachments = news.attachments.Count == 0 ? new List<Models.Attachments>() : news.attachments.ConvertAll(new Converter<Attachments, Models.Attachments>(AttachmentToModelAttachment)),
                shares = news.share.share_count,
               // priority= news.priority
            };

        }

        private static Models.Attachments AttachmentToModelAttachment(Attachments attach)
        {
            return new Models.Attachments
            {
                link = attach.link,
                photo = attach.photo,
                text = attach.text
            };
        }
    }
    
}
