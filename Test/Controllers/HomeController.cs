using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test.Controllers
{
    public class HomeController : TestBaseController
    {
        public ActionResult Index()
        {
            ViewBag.UserNameSurname = UserInfos != null ? $"Hoş Geldin {UserInfos.name}{ UserInfos.surname}" : null;
            return View();
        }
        [UserAuthorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.UserNameSurname = $"Hoş Geldin {UserInfos.name}{ UserInfos.surname}";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}