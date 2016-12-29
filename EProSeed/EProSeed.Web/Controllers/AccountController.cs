using System;
using System.Web.Mvc;
using EProSeed.Web.Models;
using System.Web.Security;
using EProSeed.Lib.BLL;
using System.Web;

namespace EProSeed.Web.Controllers
{
    public class AccountController : Controller
    {
        protected readonly ITrainer _Trainer;

        public AccountController()
        {
            _Trainer = new Trainer();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(vmLogin model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserType userType;
                    var User = _Trainer.Login(model.Email, model.Password, out userType);
                    if (User!=null)
                    {
                        CreateSession(userType, User);
                       
                        FormsAuthentication.SetAuthCookie(
                                User.Email, false);

                        FormsAuthenticationTicket ticket1 =
                           new FormsAuthenticationTicket(
                                1,                                   
                                User.Email,  
                                DateTime.Now,                        
                                DateTime.Now.AddMinutes(20),        
                                false, User.Email);
                        HttpCookie cookie1 = new HttpCookie(
                          FormsAuthentication.FormsCookieName,
                          FormsAuthentication.Encrypt(ticket1));
                        Response.Cookies.Add(cookie1);
                        FormsAuthentication.RedirectFromLoginPage(User.Email, true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                    ViewData["Error"] = "Invalid Email or Password.";
            }

            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
            }
            return View(model);
        }

        private void CreateSession(UserType userType, EProSeed.Models.TrainerModel user)
        {
            Session["Name"] = user.Name;
            Session["UserType"] = userType.ToString();
            Session["UserId"] = user.Id.ToString();
            Session["UserEmailId"] = user.Email.ToString();
        }

        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            return Redirect("/account/login");
        }

    }
}
