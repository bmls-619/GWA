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
    public class BookController : BaseController
    {
         //
        // GET: /Book/

        private BookRepository bookRepository;

        public BookController()
        {
            this.bookRepository = new BookRepository(new GContext());
        }

        //
        // GET: /Book/
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Index()
        {
            //return View(db.Students.ToList());

            var bookList = from book in bookRepository.List select book;
            var books = new List<Book>();

            if (bookList.Any())
            {
                foreach (var b in bookList)
                {
                    books.Add
                    (
                         new Book()
                         {
                              BookId = b.BookId,
                              Book_Name = b.Book_Name,
                              ISBN10 = b.ISBN10,
                              ISBN13 = b.ISBN13,
                              Pages = b.Pages,
                              Publisher = b.Publisher,
                              Language = b.Language
                         }
                    );
                }
            }

            return View(books);
        }

        //
        // GET: /Book/Details/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Details(int id = 0)
        {
            var bookDetail = bookRepository.FindById(id);
            var book = new Book();

            if (bookDetail != null)
            {
                book.BookId = bookDetail.BookId;
                book.Book_Name = bookDetail.Book_Name;
                book.ISBN10 = bookDetail.ISBN10;
                book.ISBN13 = bookDetail.ISBN13;
                book.Pages = bookDetail.Pages;
                book.Publisher = bookDetail.Publisher;
                book.Language = bookDetail.Language;
            }
            else
            {
                return HttpNotFound();
            }

            return PartialView("_Details",book);
        }

        //
        // GET: /Book/Create
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        //
        // POST: /Book/Create
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book insertingBook)
        {
            var book = new Book();

            if (ModelState.IsValid)
            {
                if (insertingBook != null)
                {

                    book.BookId = insertingBook.BookId;
                    book.Book_Name = insertingBook.Book_Name.ToUpper();
                    book.ISBN10 = insertingBook.ISBN10;
                    book.ISBN13 = insertingBook.ISBN13;
                    book.Pages = insertingBook.Pages;
                    book.Publisher = insertingBook.Publisher.ToUpper();
                    book.Language = insertingBook.Language.ToUpper();
                }
                bookRepository.Add(book);
                return Json(new { success = true });
            }

            return PartialView("_Create",insertingBook);
        }

        //
        // GET: /Book/Edit/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Edit(int id = 0)
        {
            var student = bookRepository.FindById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Edit",student);
        }

        //
        // POST: /Book/Edit/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book bookEdit, int id = 0)
        {
            var book = bookRepository.FindById(id);

            if (ModelState.IsValid)
            {

                book.BookId = bookEdit.BookId;
                book.Book_Name = bookEdit.Book_Name.ToUpper();
                book.ISBN10 = bookEdit.ISBN10;
                book.ISBN13 = bookEdit.ISBN13;
                book.Pages = bookEdit.Pages;
                book.Publisher = bookEdit.Publisher.ToUpper();
                book.Language = bookEdit.Language.ToUpper();

                bookRepository.Update(book);

                return Json(new {success = true });
            }
            return PartialView("_Edit",bookEdit);
        }

        //
        // GET: /Book/Delete/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Delete(int id = 0)
        {
            var book = bookRepository.FindById(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //
        // POST: /Book/Delete/5
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var book = bookRepository.FindById(id);

            if (book != null)
            {
                bookRepository.Delete(book);
            }

            return RedirectToAction("Index");
        }
    }
}