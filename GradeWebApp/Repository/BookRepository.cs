using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using GradeWebApp.Models;
using GradeWebApp.Repository;
using GradeWebApp.DAL;


namespace GradeWebApp.Repository
{
    public class BookRepository : IRepository<Book>
    {
        private GContext _db;

        public BookRepository(GContext db)
        {
            this._db = db;
        }

        public IEnumerable<Book> List
        {
            get
            {
                return _db.Books.ToList();
            }
        }

        public void Add(Book entity)
        {
            _db.Books.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(Book entity)
        {
            _db.Books.Remove(entity);
            _db.SaveChanges();
        }

        public void Update(Book entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public Book FindById(int Id)
        {
            var result = (from r in _db.Books where r.BookId == Id select r).FirstOrDefault();
            return result;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}