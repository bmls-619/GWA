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
using Newtonsoft.Json;

namespace GradeWebApp.Controllers
{
    public class GradeController : BaseController
    {
        private GradeRepository gradeRepository;
        private StudentRepository studentRepository;
        private BookRepository bookRespository;
        private ChapterRepository chapterRepository;

        public GradeController()
        {
            this.gradeRepository = new GradeRepository(new GContext());
            this.studentRepository = new StudentRepository(new GContext());
            this.bookRespository = new BookRepository(new GContext());
            this.chapterRepository = new ChapterRepository(new GContext());
        }
        //
        // GET: /Grade/
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Index()
        {
            //var gradeList = from grade in gradeRepository.List select grade;

            return View(gradeRepository.List.OrderBy(g => g.GradeId));
        }

        //
        // GET: /Grade/Details/5
        //[Authorize(Roles = "Admin, User")]
        //public ActionResult Details(int id = 0)
        //{
        //    var gradeDetail = gradeRepository.FindById(id);
        //    var grade = new Grade();

        //    if (gradeDetail != null)
        //    {
        //        grade.GradeId = gradeDetail.GradeId;
        //        grade.Student_ID = gradeDetail.Student_ID;
        //        grade.Book_ID = gradeDetail.Book_ID;
        //        grade.Homework = gradeDetail.Homework;
        //        grade.Excercise = gradeDetail.Excercise;
        //        grade.Participation = gradeDetail.Participation;

        //    }
        //    else
        //    {
        //        return HttpNotFound();
        //    }

        //    return PartialView(grade);
        //}

        public JsonResult SelectChapter(int  id = 0)
        {
             var chapter = chapterRepository.List.Where(c => c.BookID == id);
             return Json(chapter, JsonRequestBehavior.AllowGet);
        }

        public JsonResult StudentDetail(int studentID)
        {
            string studentDtl = string.Empty;

            var studentDetail = studentRepository.FindById(studentID);

            try
            {

                studentDtl = string.Format("{0} {1} {2}",

                    //"Student ID: " + studentDetail.StudentId + Environment.NewLine,
                    "Fullname: " + studentDetail.Name.Trim(), studentDetail.LastName.Trim() + Environment.NewLine,
                    "       Level: " + studentDetail.Level
                );

                return Json(studentDtl);
            }
            catch(Exception)
            {
                studentDtl = "Student ID: " + studentID + " was not found!";
            }
            return Json(studentDtl);
        }

        //
        // GET: /Grade/Create
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Create()
        {
            var studentList = from student in studentRepository.List select student;
            var bookList = from book in bookRespository.List select book;
            var chapterList = from chapter in chapterRepository.List select chapter;


            ViewBag.Student_ID = new SelectList(studentList, "StudentId", "Name");
            ViewBag.Book_ID = new SelectList(bookList, "BookId", "Book_Name");
            ViewBag.Chapter_ID = new SelectList(chapterList, "ChapterID", "ChapterDescription");
            return PartialView("_Create");
        }

        // POST: /Grade/Create
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Grade insertingGrade)
        {
            var grade = new Grade();

            var studentList = from student in studentRepository.List select student;
            var bookList = from book in bookRespository.List select book;
            var chapterList = from chapter in chapterRepository.List select chapter;

            var studentExist = studentRepository.List.Where(s => s.StudentId == insertingGrade.Student_ID).Count();
            var assignedChapter = gradeRepository.List.Where(g => g.Student_ID == insertingGrade.Student_ID && g.Book_ID == insertingGrade.Book_ID && g.Chapter_ID == insertingGrade.Chapter_ID).Count();

            if(studentExist == 0)
            {
                ViewBag.studentExist = "The Student ID: " + insertingGrade.Student_ID + " was not found";
                ViewBag.Student_ID = new SelectList(studentList, "StudentId", "Name");
                ViewBag.Book_ID = new SelectList(bookList, "BookId", "Book_Name");
                ViewBag.Chapter_ID = new SelectList(chapterList, "ChapterID", "ChapterDescription");
                return PartialView("_Create", insertingGrade);
            }

            if (assignedChapter > 0)
            {
                ViewBag.assignedChapter = "This Student has the Chapter: " + insertingGrade.Chapter_ID + " of the Book " + insertingGrade.Book_ID + " assigned already";
                ViewBag.Student_ID = new SelectList(studentList, "StudentId", "Name");
                ViewBag.Book_ID = new SelectList(bookList, "BookId", "Book_Name");
                ViewBag.Chapter_ID = new SelectList(chapterList, "ChapterID", "ChapterDescription");
                return PartialView("_Create", insertingGrade);
            }

            if (ModelState.IsValid)
            {
                if (insertingGrade != null)
                {
                    grade.Student_ID = insertingGrade.Student_ID;
                    grade.Book_ID = insertingGrade.Book_ID;
                    grade.Chapter_ID = insertingGrade.Chapter_ID;
                    grade.Homework = insertingGrade.Homework;
                    grade.Excercise = insertingGrade.Excercise;
                    grade.Participation = insertingGrade.Participation;
                    grade.CreateDate = DateTime.Now;
                }
                gradeRepository.Add(grade);

                return Json(new {success = true });
            }

            //var studentList = from student in studentRepository.List select student;
            //var bookList = from book in bookRespository.List select book;
            //var chapterList = from chapter in chapterRepository.List select chapter;


            ViewBag.Student_ID = new SelectList(studentList, "StudentId", "Name");
            ViewBag.Book_ID = new SelectList(bookList, "BookId", "Book_Name");
            ViewBag.Chapter_ID = new SelectList(chapterList, "ChapterID", "ChapterDescription");            
            return PartialView("_Create",insertingGrade);
        }

        //
        // GET: /Grade/Edit/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Edit(int id = 0)
        {
            var grade = gradeRepository.FindById(id);
            if (grade == null)
            {
                return HttpNotFound();
            }

            var studentList = from student in studentRepository.List select student;
            var bookList = from book in bookRespository.List select book;
            var chapterList = from chapter in chapterRepository.List select chapter;

            ViewBag.Student_ID = new SelectList(studentList, "StudentId", "Name", grade.Student_ID);
            ViewBag.Book_ID = new SelectList(bookList, "BookId", "Book_Name", grade.Book_ID);
            ViewBag.Chapter_ID = new SelectList(chapterList, "ChapterID", "ChapterDescription");
            return PartialView("_Edit",grade);
        }

        //
        // POST: /Grade/Edit/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Grade gradeEdit, int id = 0)
        {
            var grade = gradeRepository.FindById(id);
            var studentList = from student in studentRepository.List select student;
            var bookList = from book in bookRespository.List select book;
            var chapterList = from chapter in chapterRepository.List select chapter;

            if (ModelState.IsValid)
            {
                grade.Student_ID = gradeEdit.Student_ID;
                grade.Book_ID = gradeEdit.Book_ID;
                grade.Chapter_ID = gradeEdit.Chapter_ID;
                grade.Homework = gradeEdit.Homework;
                grade.Excercise = gradeEdit.Excercise;
                grade.Participation = gradeEdit.Participation;
                grade.CreateDate = DateTime.Now;

                gradeRepository.Update(grade);

                return Json(new { success = true });
            }

            ViewBag.Student_ID = new SelectList(studentList, "StudentId", "Name", grade.Student_ID);
            ViewBag.Book_ID = new SelectList(bookList, "BookId", "Book_Name", grade.Book_ID);
            ViewBag.Chapter_ID = new SelectList(chapterList, "ChapterID", "ChapterDescription");

            return PartialView("_Edit",grade);
        }


        // GET: /Grade/Delete/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Delete(int id = 0)
        {
            var grade = gradeRepository.FindById(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }


        //
        // POST: /Grade/Delete/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var grade = gradeRepository.FindById(id);

            if (grade != null)
            {
                gradeRepository.Delete(grade);
            }

            return RedirectToAction("Index");
        }
    }
}


 //private GContext db = new GContext();

 //       //
 //       // GET: /Grade/

 //       public ActionResult Index()
 //       {
 //           var grades = db.Grades.Include(g => g.student).Include(g => g.book);
 //           return View(grades.ToList());
 //       }

 //       //
 //       // GET: /Grade/Details/5

 //       public ActionResult Details(int id = 0)
 //       {
 //           Grade grade = db.Grades.Find(id);
 //           if (grade == null)
 //           {
 //               return HttpNotFound();
 //           }
 //           return View(grade);
 //       }

 //       //
 //       // GET: /Grade/Create

 //       public ActionResult Create()
 //       {
 //           ViewBag.Student_ID = new SelectList(db.Students, "StudentId", "Name");
 //           ViewBag.Book_ID = new SelectList(db.Books, "BookId", "Book_Description");
 //           return View();
 //       }

 //       //
 //       // POST: /Grade/Create

 //       [HttpPost]
 //       [ValidateAntiForgeryToken]
 //       public ActionResult Create(Grade grade)
 //       {
 //           if (ModelState.IsValid)
 //           {
 //               db.Grades.Add(grade);
 //               db.SaveChanges();
 //               return RedirectToAction("Index");
 //           }

 //           ViewBag.Student_ID = new SelectList(db.Students, "StudentId", "Name", grade.Student_ID);
 //           ViewBag.Book_ID = new SelectList(db.Books, "BookId", "Book_Description", grade.Book_ID);
 //           return View(grade);
 //       }

 //       //
 //       // GET: /Grade/Edit/5

 //       public ActionResult Edit(int id = 0)
 //       {
 //           Grade grade = db.Grades.Find(id);
 //           if (grade == null)
 //           {
 //               return HttpNotFound();
 //           }
 //           ViewBag.Student_ID = new SelectList(db.Students, "StudentId", "Name", grade.Student_ID);
 //           ViewBag.Book_ID = new SelectList(db.Books, "BookId", "Book_Description", grade.Book_ID);
 //           return View(grade);
 //       }

 //       //
 //       // POST: /Grade/Edit/5

 //       [HttpPost]
 //       [ValidateAntiForgeryToken]
 //       public ActionResult Edit(Grade grade)
 //       {
 //           if (ModelState.IsValid)
 //           {
 //               db.Entry(grade).State = EntityState.Modified;
 //               db.SaveChanges();
 //               return RedirectToAction("Index");
 //           }
 //           ViewBag.Student_ID = new SelectList(db.Students, "StudentId", "Name", grade.Student_ID);
 //           ViewBag.Book_ID = new SelectList(db.Books, "BookId", "Book_Description", grade.Book_ID);
 //           return View(grade);
 //       }

 //       //
 //       // GET: /Grade/Delete/5

 //       public ActionResult Delete(int id = 0)
 //       {
 //           Grade grade = db.Grades.Find(id);
 //           if (grade == null)
 //           {
 //               return HttpNotFound();
 //           }
 //           return View(grade);
 //       }

 //       //
 //       // POST: /Grade/Delete/5

 //       [HttpPost, ActionName("Delete")]
 //       [ValidateAntiForgeryToken]
 //       public ActionResult DeleteConfirmed(int id)
 //       {
 //           Grade grade = db.Grades.Find(id);
 //           db.Grades.Remove(grade);
 //           db.SaveChanges();
 //           return RedirectToAction("Index");
 //       }

 //       protected override void Dispose(bool disposing)
 //       {
 //           db.Dispose();
 //           base.Dispose(disposing);
 //       }