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
    public class StudentController : BaseController
    {
        //
        // GET: /Student/

        private StudentRepository studentRepository;

        public StudentController()
        {
            this.studentRepository = new StudentRepository(new GContext());
        }

        //
        // GET: /Student/
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Index()
        {
            //return View(db.Students.ToList());

            var studentList = from student in studentRepository.List select student;
            var students = new List<Student>();

            if (studentList.Any())
            {
                foreach (var sl in studentList)
                {
                    students.Add
                    (
                         new Student()
                         {
                             StudentId = sl.StudentId,
                             Name = sl.Name,
                             LastName = sl.LastName,
                             Level = sl.Level
                         }
                    );
                }
            }

            return View(students);
        }

        //
        // GET: /Student/Details/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Details(int id = 0)
        {
            var studentDetail = studentRepository.FindById(id);
            var student = new Student();

            if (studentDetail != null)
            {
                student.StudentId = studentDetail.StudentId;
                student.Name = studentDetail.Name;
                student.LastName = studentDetail.LastName;
                student.Level = studentDetail.Level;
            }
            else
            {
                return HttpNotFound();
            }

            return PartialView("_Details", student);
        }

        //
        // GET: /Student/Create
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        //
        // POST: /Student/Create
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student insertingStudent)
        {
            var student = new Student();

            if (ModelState.IsValid)
            {
                if (insertingStudent != null)
                {
                    student.StudentId = insertingStudent.StudentId;
                    student.Name = insertingStudent.Name.ToUpper();
                    student.LastName = insertingStudent.LastName.ToUpper();
                    student.Level = insertingStudent.Level;
                }
                studentRepository.Add(student);

                return Json(new {success = true });
            }

            return PartialView("_Create",insertingStudent);
        }

        //
        // GET: /Student/Edit/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Edit(int id = 0)
        {
            var student = studentRepository.FindById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Edit",student);
        }

        //
        // POST: /Student/Edit/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student studentEdit, int id = 0)
        {
            var student = studentRepository.FindById(id);

            if (ModelState.IsValid)
            {
                student.StudentId = studentEdit.StudentId;
                student.Name = studentEdit.Name.ToUpper();
                student.LastName = studentEdit.LastName.ToUpper();
                student.Level = studentEdit.Level;

                studentRepository.Update(student);

                return Json(new {success = true });
            }
            return PartialView("_Edit",studentEdit);
        }

        //
        // GET: /Student/Delete/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Delete(int id = 0)
        {
            var student = studentRepository.FindById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // POST: /Student/Delete/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var student = studentRepository.FindById(id);

            if (student != null)
            {
                studentRepository.Delete(student);
            }

            return RedirectToAction("Index");
        }
    }
}