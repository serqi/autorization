using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Test.Models;

namespace Test.Controllers
{
    public class LoginController : TestBaseController
    {
        List<User> _userlar { get; set; }
        List<User> Userlar
        {
            get
            {
                if (_userlar == null)
                {
                    _userlar = new List<User>();
                    _userlar.Add(new User() { id = 0, password = "12345", username = "fahrettin", name = "Fahrettin", surname = "Kapdan", roles = new UserInRole() { RoleID = 1, UserID = 0 } });
                }
                return _userlar;
            }

        }


        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index");
            var usr = new UserProfileSignUp
            {
                Username = "",
                Password = "",
                Roles = new List<SelectListItem>()
            };

            usr.Roles.Add(new SelectListItem
            {
                Text = "Admin",
                Value = "1"
            });

            return View(usr);
        }
        [HttpPost]
        public ActionResult SignUp(UserProfileSignUp userProfileSignUp)
        {
            if (!ModelState.IsValid)
            {
                userProfileSignUp.Roles = new List<SelectListItem>();
                userProfileSignUp.Roles.Add(new SelectListItem
                {
                    Text = "Admin",
                    Value = "1"
                });
                return View(userProfileSignUp);
            }
            else
            {
                var user = new User
                {
                    username = userProfileSignUp.Username,
                    password = userProfileSignUp.Password,
                };

                //new UserBLL().Ekle(user);

                var userInRole = new UserInRole
                {
                    UserID = user.id,
                    RoleID = userProfileSignUp.RoleID
                };

                //new UserInRoleBLL().Ekle(userInRole);

                FormsAuthentication.SetAuthCookie(userProfileSignUp.Username, false);

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Index(Login model, string returnUrl = "")
        {
            var userBll = Userlar;
            if (ModelState.IsValid)
            {
                var user = userBll.Find(x => x.username == model.UserName && x.password == model.Password);// .GetirByUsernameAndPassword(userProfile.Username, userProfile.Password);
                if (user != null)
                {
                    //if (userBll.GetirRolesByUserName(user.username).Count == 0)
                    //{
                    //    ViewBag.loginError = "Bu kullanıcının yetkileri yok";
                    //    return View(userProfile);
                    //}
                    if (user.roles.RoleID != 1)
                    {
                        ViewBag.loginError = "Bu kullanıcının yetkileri yok";
                        return View(model);
                    }
                    //FormsAuthentication.SetAuthCookie(user.Username, user.RememberMe);
                    FormsAuthentication.SetAuthCookie($"{user.id}|{user.username}|{user.name}|{user.surname}|{user.roles.RoleID}", false);//set token for user

                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.loginError = "Kullanıcı adı veya şifre hatalı girildi.";
                    return View(model);
                }
            }
            else
            {
                ViewBag.loginError = "";
                return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login", null);
        }
    }
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }

        public UserInRole roles { get; set; }
    }
    public class UserInRole
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }

}
