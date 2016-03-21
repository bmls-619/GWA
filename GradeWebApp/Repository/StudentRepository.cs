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
    public class StudentRepository : IRepository<Student>
    {
        private GContext _db;

        public StudentRepository(GContext db)
        {
            this._db = db;
        }

        public IEnumerable<Student> List
        {
            get
            {
                return _db.Students.ToList();
            }
        }

        public void Add(Student entity)
        {
            _db.Students.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(Student entity)
        {
            _db.Students.Remove(entity);
            _db.SaveChanges();
        }

        public void Update(Student entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public Student FindById(int Id)
        {
            var result = (from r in _db.Students where r.StudentId == Id select r).FirstOrDefault();
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