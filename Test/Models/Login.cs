using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test.Models
{
    public class Login
    {
        [Display(Name = "User name")]
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class UserProfile
    {
        [Display(Name = "Kullanıcı Adı"), Required]
        public string Username { get; set; }

        [Display(Name = "Şifre"), Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class UserProfileSignUp : UserProfile
    {
        //[Remote("IsUserNameAvailable", "Home", ErrorMessage = "Bu kullanıcı adı zaten kayıtlı"), CmUsername]//Remote Client Side çalışıyor,CMUsername Server side çalışıyor

        [Display(Name = "Kullanıcı Adı"), Required]
        public new string Username { get; set; }

        //[Compare("Password", ErrorMessage = "Şifrenizle uyuşmuyor")]
        [Display(Name = "Şifre Tekrar"), DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Rolü")]
        public int RoleID { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }
}