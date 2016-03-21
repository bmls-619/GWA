//using GradeWebApp.DAL.Security;
//using GradeWebApp.DAL;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using GradeWebApp.Models;
//using GradeWebApp.Repository;

//namespace GradeWebApp.Controllers
//{
//    public class RoleController : BaseController
//    {
//        private RoleRepository roleRepository;

//        public RoleController()
//        {
//            this.roleRepository = new RoleRepository(new GContext());
//        }

//        //
//        // GET: /Student/
//        [Authorize(Roles = "Admin")]
//        public ActionResult Index()
//        {
//            //return View(db.Students.ToList());

//            var roleList = from role in roleRepository.List select role;
//            var roles = new List<Role>();

//            if (roleList.Any())
//            {
//                foreach (var rl in roleList)
//                {
//                     roles.Add
//                    (
//                         new Role()
//                         {
//                             RoleId = rl.RoleId,
//                             RoleName = rl.RoleName
//                         }
//                    );
//                }
//            }

//            return View(roles);
//        }

//        //
//        // GET: /Student/Details/5
//        [Authorize(Roles = "Admin")]
//        public ActionResult Details(int id = 0)
//        {
//            var roleDetail = roleRepository.FindById(id);
//            var role = new Role();

//            if (roleDetail != null)
//            {
//                role.RoleId = roleDetail.RoleId;
//                //role.Description = roleDetail.Description;

//            }
//            else
//            {
//                return HttpNotFound();
//            }

//            return View(role);
//        }

//        //
//        // GET: /Student/Create
//        [Authorize(Roles = "Admin")]
//        public ActionResult Create()
//        {
//            return View();
//        }

//        //
//        // POST: /Student/Create
//        [Authorize(Roles = "Admin")]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(Role insertingRole)
//        {
//            var role = new Role();

//            if (ModelState.IsValid)
//            {
//                if (insertingRole != null)
//                {
//                    role.RoleId = insertingRole.RoleId;
//                    //role.Description = insertingRole.Description;
                    
//                }
//                roleRepository.Add(role);

//                return RedirectToAction("Index");
//            }

//            return View(insertingRole);
//        }

//        //
//        // GET: /Student/Edit/5
//        [Authorize(Roles = "Admin")]
//        public ActionResult Edit(int id = 0)
//        {
//            var role = roleRepository.FindById(id);
//            if (role == null)
//            {
//                return HttpNotFound();
//            }
//            return View(role);
//        }

//        //
//        // POST: /Student/Edit/5
//        [Authorize(Roles = "Admin")]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(Role roleEdit, int id = 0)
//        {
//            var role = roleRepository.FindById(id);

//            if (ModelState.IsValid)
//            {
//                role.RoleId = roleEdit.RoleId;
//                //role.Description = roleEdit.Description;

//                roleRepository.Update(role);

//                return RedirectToAction("Index");
//            }
//            return View(roleEdit);
//        }

//        //
//        // GET: /Student/Delete/5
//        [Authorize(Roles = "Admin")]
//        public ActionResult Delete(int id = 0)
//        {
//            var role = roleRepository.FindById(id);
//            if (role == null)
//            {
//                return HttpNotFound();
//            }
//            return View(role);
//        }

//        //
//        // POST: /Student/Delete/5
//        [Authorize(Roles = "Admin")]
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            var student = roleRepository.FindById(id);

//            if (student != null)
//            {
//                roleRepository.Delete(student);
//            }

//            return RedirectToAction("Index");
//        }
//    }
//}