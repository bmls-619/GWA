using GradeWebApp.DAL.Security;
using GradeWebApp.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradeWebApp.Models;
using GradeWebApp.Repository;

namespace GradeWebApp.Controllers
{

    [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
    [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
    [CustomAuthorize(Roles = "Admin,User")]
    [CustomAuthorize(Users = "1,2")]
    public class UserController : BaseController
    {
         private UserRepository userRepository;

        public UserController()
        {
            this.userRepository = new UserRepository(new GContext());
        }

        
       // GET: /User/
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Index()
        {
            //return View(db.Students.ToList());

            var userList = from user in userRepository.List select user;
            var users = new List<User>();

            if (userList.Any())
            {
                foreach (var ul in userList)
                {
                     users.Add
                    (
                         new User()
                         {
                             UserId = ul.UserId,
                             Username = ul.Username,
                             Email = ul.Email,
                             Password = ul.Password,
                             RepeatPassword = ul.RepeatPassword,
                             FirstName = ul.FirstName,
                             LastName = ul.LastName,
                             //RoleId = ul.RoleId,
                             IsActive = ul.IsActive,
                             CreateDate = ul.CreateDate
                         }
                    );
                }
            }

            return View(users);
        }

        
        // GET: /User/Details/5
        //[Authorize(Roles = "Admin")]
        //public ActionResult Details(int id = 0)
        //{
        //    var userDetail = userRepository.FindById(id);
        //    var user = new User();

        //    if (userDetail != null)
        //    {
        //        user.UserId = userDetail.UserId;
        //        user.Username = userDetail.Username;
        //        user.Email = userDetail.Email;
        //        user.Password = userDetail.Password;
        //        user.RepeatPassword = userDetail.RepeatPassword;
        //        user.FirstName = userDetail.FirstName;
        //        user.LastName = userDetail.LastName;
        //        user.IsActive = userDetail.IsActive;
        //        user.CreateDate = userDetail.CreateDate;

        //    }
        //    else
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(user);
        //}


        //
        //GET: /User/Create
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        //
        // POST: /User/Create
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User insertingUser)
        {
            var user = new User();

            if (ModelState.IsValid)
            {
                if (insertingUser != null)
                {
                    user.UserId = insertingUser.UserId;
                    user.Username = insertingUser.Username;
                    user.Email = insertingUser.Email;
                    user.Password = insertingUser.Password;
                    user.RepeatPassword = insertingUser.RepeatPassword;
                    user.FirstName = insertingUser.FirstName;
                    user.LastName = insertingUser.LastName;
                    user.IsActive = insertingUser.IsActive;
                    user.CreateDate = DateTime.Now;
                }
                userRepository.Add(user);
                return Json(new { success = true });
            }

            return PartialView("_Create",insertingUser);
        }

        //
        // GET: /User/Edit/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Edit(int id = 0)
        {
            var user = userRepository.FindById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Edit",user);
        }

        //
        // POST: /User/Edit/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User userEdit, int id = 0)
        {
            var user = userRepository.FindById(id);

            if (ModelState.IsValid)
            {
                user.UserId = userEdit.UserId;
                user.Username = userEdit.Username;
                user.Email = userEdit.Email;
                user.Password = userEdit.Password;
                user.RepeatPassword = userEdit.RepeatPassword;
                user.FirstName = userEdit.FirstName;
                user.LastName = userEdit.LastName;
                user.IsActive = userEdit.IsActive;
                user.CreateDate = DateTime.Now;
                user.LoginAttempt = 0;
                user.IsLocked = userEdit.IsLocked;
                

                userRepository.Update(user);
                return Json(new { success = true });
            }
            return PartialView("_Edit",userEdit);
        }

        //
        // GET: /User/Delete/5
        //[CustomAuthorize(Roles = "Admin")]
        //public ActionResult Delete(int id = 0)
        //{
        //    var user = userRepository.FindById(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}

        //
        // POST: /User/Delete/5
        //[Authorize(Roles = "Admin")]
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var user = userRepository.FindById(id);

        //    if (user != null)
        //    {
        //        userRepository.Delete(user);
        //    }

        //    return RedirectToAction("Index");
        //}
    }
}