//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using GradeWebApp.Models;
//using GradeWebApp.Context;
//using GradeWebApp.Repository;

//namespace GradeWebApp.Controllers
//{
//    public class UserRoleController : Controller
//    {

//        private UserRoleRepository userRoleRepository;
//        private RoleRepository roleRepository;
//        private UserRepository userRepository;

//        public UserRoleController()
//        {
//            this.userRoleRepository = new UserRoleRepository(new GContext());
//            this.roleRepository = new RoleRepository(new GContext());
//            this.userRoleRepository = new UserRoleRepository(new GContext());
//        }

//        //
//        // GET: /UserRole/

//        public ActionResult Index()
//        {
//            var userRoleList = from userRole in userRoleRepository.List select userRole;
//            return View(userRoleList);
//        }

//        //
//        // GET: /UserRole/Details/5

//        public ActionResult Details(int id = 0)
//        {
//           var userRoleDetail = userRoleRepository.FindById(id);
//            var userRole = new UserRole();

//            if (userRoleDetail != null)
//            {
//                userRole.UserRoleId = userRoleDetail.UserRoleId;
//                userRole.UserId = userRoleDetail.UserId;
//                userRole.RoleId = userRoleDetail.RoleId;
//            }
//            else
//            {
//                return HttpNotFound();
//            }

//            return View(userRole);
//        }

//        //
//        // GET: /UserRole/Create

//        public ActionResult Create()
//        {
//            var userD = from user in userRepository.List select user;
//            var rol = from role in roleRepository.List select role;
            

//            ViewBag.UserId = new SelectList(userD, "UserId", "Username");
//            ViewBag.RoleId = new SelectList(rol, "RoleId", "RoleName");
//            return View();
//        }

//        //
//        // POST: /UserRole/Create

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(UserRole insertingUserRole)
//        {
//            var userRole = new UserRole();

//            if (insertingUserRole != null)
//            {
//                userRole.UserRoleId = insertingUserRole.UserRoleId;
//                userRole.UserId = insertingUserRole.UserId;
//                userRole.RoleId = insertingUserRole.RoleId;

//            }
//            userRoleRepository.Add(userRole);

//            var userD = from user in userRepository.List select user;
//            var rol = from role in roleRepository.List select role;

//            ViewBag.UserId = new SelectList(userD, "UserId", "Username", userRole.UserId);
//            ViewBag.RoleId = new SelectList(rol, "RoleId", "RoleName", userRole.RoleId);
//            return View(insertingUserRole);
//        }

//        //
//        // GET: /UserRole/Edit/5

//        public ActionResult Edit(int id = 0)
//        {
//            var userRole = userRoleRepository.FindById(id);
//            if (userRole == null)
//            {
//                return HttpNotFound();
//            }

//            var userD = from user in userRepository.List select user;
//            var rol = from role in roleRepository.List select role;

//            ViewBag.UserId = new SelectList(userD, "UserId", "Username", userRole.UserId);
//            ViewBag.RoleId = new SelectList(rol, "RoleId", "RoleName", userRole.RoleId);
//            return View(userRole);
//        }

//        //
//        // POST: /UserRole/Edit/5

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(UserRole userRoleEdit, int id=0)
//        {
//            var userRole = userRoleRepository.FindById(id);

//            if (ModelState.IsValid)
//            {
//                userRole.UserRoleId = userRoleEdit.UserRoleId;
//                userRole.UserId = userRoleEdit.UserId;
//                userRole.RoleId = userRoleEdit.RoleId;
//            }
//            userRoleRepository.Update(userRole);

//            var userD = from user in userRepository.List select user;
//            var rol = from role in roleRepository.List select role;

//            ViewBag.UserId = new SelectList(userD, "UserId", "Username", userRole.UserId);
//            ViewBag.RoleId = new SelectList(rol, "RoleId", "RoleName", userRole.RoleId);
//            //return View(userrole);

//            return RedirectToAction("Index");
//        }

//        //
//        // GET: /UserRole/Delete/5

//        public ActionResult Delete(int id = 0)
//        {
//            var userRole = userRoleRepository.FindById(id);
//            if (userRole == null)
//            {
//                return HttpNotFound();
//            }
//            return View(userRole);
//        }

//        //
//        // POST: /UserRole/Delete/5

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            var userRole = userRoleRepository.FindById(id);

//            if (userRole != null)
//            {
//                userRoleRepository.Delete(userRole);
//            }

//            return RedirectToAction("Index");
//        }
//    }
//}