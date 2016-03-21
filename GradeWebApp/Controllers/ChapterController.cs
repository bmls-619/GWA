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
    public class ChapterController : BaseController
    {
        //
        // GET: /Chapter/

        private BookRepository bookRepository;
        private ChapterRepository chapterRepository;

        public ChapterController()
        {
            this.bookRepository = new BookRepository(new GContext());
            this.chapterRepository = new ChapterRepository(new GContext());
        }

        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Index()
        {
            var chapterList = from chapter in chapterRepository.List select chapter;
            var chapters = new List<Chapter>();

            if(chapterList.Any())
            {
                foreach(var c in chapterList)
                {
                    chapters.Add
                    (
                        new Chapter()
                        {
                            ChapterID = c.ChapterID,
                            BookID = c.BookID,        
                            ChapterDescription = c.ChapterDescription
                        }
                    );
                }
            }

            return View(chapters);
        }

        //
        // GET: /Chapter/Details/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Details(int id = 0)
        {
            var chapterDetail = chapterRepository.FindById(id);
            var chapter = new Chapter();

            if (chapterDetail != null)
            {
                chapter.ChapterID = chapterDetail.ChapterID;
                chapter.BookID = chapterDetail.BookID;
                chapter.ChapterDescription = chapterDetail.ChapterDescription;
            }
            else
            {
                return HttpNotFound();
            }
            return PartialView("_Details", chapter);
        }

        //
        // GET: /Chapter/Create
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Create()
        {
            var bookList = from book in bookRepository.List select book;
            ViewBag.BookID = new SelectList(bookList, "BookId", "Book_Name");
            return PartialView("_Create");
        }

        //
        // POST: /Chapter/Create
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Chapter InsertingChapter)
        {
            var chapter = new Chapter();

            if (ModelState.IsValid)
            {
               if(InsertingChapter != null)
               {
                   chapter.ChapterID = InsertingChapter.ChapterID;
                   chapter.BookID = InsertingChapter.BookID;
                   chapter.ChapterDescription = InsertingChapter.ChapterDescription.ToUpper();
               }
               chapterRepository.Add(chapter);
               return Json(new { success = true });
            }


            var bookList = from book in bookRepository.List select book;
            ViewBag.BookID = new SelectList(bookList, "BookId", "Book_Name");
            return PartialView("_Create",chapter);
        }

        //
        // GET: /Chapter/Edit/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Edit(int id = 0)
        {
            var chapter = chapterRepository.FindById(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Edit",chapter);
        }

        //
        // POST: /Chapter/Edit/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Chapter chapterEdit, int id = 0)
        {
            var chapter = chapterRepository.FindById(id);

            if (ModelState.IsValid)
            {
                chapter.ChapterID = chapterEdit.ChapterID;
                chapter.BookID = chapterEdit.BookID;
                chapter.ChapterDescription = chapterEdit.ChapterDescription.ToUpper();

                chapterRepository.Update(chapter);

                return Json(new { success = true });
            }
            return PartialView("_Edit",chapter);
        }

        //
        // GET: /Chapter/Delete/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Delete(int id = 0)
        {
            var chapter = chapterRepository.FindById(id);
            if (chapter == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete",chapter);
        }

        //
        // POST: /Chapter/Delete/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var chapter = chapterRepository.FindById(id);
            chapterRepository.Delete(chapter);

            return Json(new { success = true });
        }
    }
}