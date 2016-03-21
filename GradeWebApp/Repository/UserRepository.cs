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
    public class UserRepository : IRepository<User>
    {
        private GContext _db;
        public UserRepository(GContext db)
        {
            this._db = db;
        }

        public IEnumerable<User> List
        {
            get
            {
                return _db.Users.ToList();
            }
        }

        public void Add(User entity)
        {
            _db.Users.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(User entity)
        {
            _db.Users.Remove(entity);
            _db.SaveChanges();
        }

        public void Update(User entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public User FindById(int Id)
        {
            var result = (from r in _db.Users where r.UserId == Id select r).FirstOrDefault();
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