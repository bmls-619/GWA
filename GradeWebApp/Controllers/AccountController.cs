using GradeWebApp.DAL.Security;
using GradeWebApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradeWebApp.Models;
using System.Web.Security;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using GradeWebApp.Stored_Procedure;


namespace GradeWebApp.Controllers
{
    public class AccountController : BaseController
    {
        GContext gContext = new GContext();
        spLoginAttempt spla = new spLoginAttempt();
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login model/*, string returnUrl =""*/)
        {

                if (ModelState.IsValid)
                {
                        var blockedUser = gContext.Users.Where(u => u.Username == model.Username/* && u.Password == model.Password*/ && u.IsLocked == true).Count();

                        if (blockedUser == 1)
                        {
                            ViewBag.blockedUser = "Your account is blocked, Contact your administrator";
                            return View();
                        }

                        var user = gContext.Users.Where(u => u.Username == model.Username && u.Password == model.Password).FirstOrDefault();
                        if (user != null)
                        {
                            var roles = user.Roles.Select(m => m.RoleName).ToArray();

                            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                            serializeModel.UserId = user.UserId;
                            serializeModel.FirstName = user.FirstName;
                            serializeModel.LastName = user.LastName;
                            serializeModel.roles = roles;

                            string userData = JsonConvert.SerializeObject(serializeModel);
                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                     1,
                                    user.Email,
                                     DateTime.Now,
                                     DateTime.Now.AddMinutes(15),
                                     false,
                                     userData);

                            string encTicket = FormsAuthentication.Encrypt(authTicket);
                            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                            Response.Cookies.Add(faCookie);

                            if (roles.Contains("Admin"))
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else if (roles.Contains("User"))
                            {
                                return RedirectToAction("Index", "Home");
                            }

                        }

                        ModelState.AddModelError("", "Wrong credentials");

                        spla.LoginAttempt(model.Username, model.Password);

            }
            return View(model);
        }

      [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Account", null);
        }

    }
}
