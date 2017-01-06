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
            //ViewBag.members =  ;

          
            return View(AppServer.Server.GetMembers("csu_iit").ConvertAll(new Converter<AppServer.User, Models.User>(ServerUserToWebUser)));
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
    }
    
}
