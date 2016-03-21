using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity.Validation;
using GradeWebApp.Models;
using GradeWebApp.Repository;
using GradeWebApp.DAL;

namespace GradeWebApp.Repository
{
    public class GradeRepository : IRepository<Grade>
    {
        private GContext _db;

        public GradeRepository(GContext db)
        {
            this._db = db;
        }

        public IEnumerable<Grade> List
        {
            get
            {
                return _db.Grades.ToList();
            }
        }

        public void Add(Grade entity)
        {
            _db.Grades.Add(entity);

            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public void Delete(Grade entity)
        {
            _db.Grades.Remove(entity);
            _db.SaveChanges();
        }

        public void Update(Grade entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public Grade FindById(int Id)
        {
            var result = (from r in _db.Grades where r.GradeId == Id select r).FirstOrDefault();
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