using System.Web.Mvc;
using AppServer;
using System;

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
        public ActionResult GetUserNews(int userId)
        {

            return View(AppServer.Server.GetNews(userId)
                .ConvertAll(new Converter<AppServer.News, Models.News>(ServerNewsToWebNews)));
        }
        //List<Point> lp = lpf.ConvertAll(
        //    new Converter<PointF, Point>(PointFToPoint));
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
                photo = news.photo
             };

        }
    }
    
}
