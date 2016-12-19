using System;
using System.Web.Mvc;
using EProSeed.Web.Models;
using System.Web.Security;
using EProSeed.Lib.BLL;

namespace EProSeed.Web.Controllers
{
    public class AccountController : Controller
    {
        protected readonly ITrainer _Trainer;

        public AccountController()
        {
            _Trainer = new Trainer();
        }


        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                        Session["Name"] = User.Name;
                        Session["UserType"] = userType.ToString();
                        Session["UserId"] = User.Id.ToString();
                        Session["UserEmailId"] = User.Email.ToString();
                        FormsAuthentication.RedirectFromLoginPage(User.Id.ToString(), false);
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


        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            return Redirect("/account/login");
        }

    }
}
