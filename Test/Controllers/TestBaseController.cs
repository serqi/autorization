using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Test.Controllers
{
    public class TestBaseController : Controller
    {
        private User _userInfos { get; set; }
        public User UserInfos
        {
            get
            {
                if (_userInfos == null)
                {
                    string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
                    HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
                    if (authCookie != null)
                    {
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it

                        string[] tokens = ticket.Name.Split('|');
                        _userInfos = new User()
                        {
                            id = Convert.ToInt32(tokens[0]),
                            name = tokens[2],
                            roles = new UserInRole() { RoleID = Convert.ToInt32(tokens[4]), UserID = Convert.ToInt32(tokens[0]) },
                            surname = tokens[3],
                            username = tokens[1]
                        };
                    }
                }
                return _userInfos;
            }
        }
    }
    public class UserAuthorize : AuthorizeAttribute
    {
        private string WebCookieName
        {
            get
            {
                return FormsAuthentication.FormsCookieName;
            }
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.Cookies[WebCookieName] != null)
            {
                string WebCookieValueEnc = httpContext.Request.Cookies[WebCookieName].Value;
                var cokieObj = FormsAuthentication.Decrypt(httpContext.Request.Cookies[WebCookieName].Value);
                if (!cokieObj.Expired)
                {
                    return true;
                }
                else
                {
                    FormsAuthentication.SignOut();
                    httpContext.Response.Redirect("/Login/Index");
                    return false;
                }
            }
            else
            {
                httpContext.Response.Redirect("/Login/Index");
                return false;
            }

        }
    }
}