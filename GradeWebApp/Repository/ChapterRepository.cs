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
    public class ChapterRepository : IRepository<Chapter>
    {
        private GContext _db;

        public ChapterRepository(GContext db)
        {
            this._db = db;

            _db.Configuration.ProxyCreationEnabled = false;
        }

        public IEnumerable<Chapter> List
        {
            get
            {
                return _db.Chapters.ToList();
            }
        }

        public void Add(Chapter entity)
        {
            _db.Chapters.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(Chapter entity)
        {
            _db.Chapters.Remove(entity);
            _db.SaveChanges();
        }

        public void Update(Chapter entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public Chapter FindById(int Id)
        {
            var result = (from r in _db.Chapters where r.ChapterID == Id select r).FirstOrDefault();
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