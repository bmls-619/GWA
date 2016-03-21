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
    public class RoleRepository : IRepository<Role>
    {
        private GContext _db;
        public RoleRepository(GContext db)
        {
            this._db = db;
        }

        public IEnumerable<Role> List
        {
            get
            {
                return _db.Roles.ToList();
            }
        }

        public void Add(Role entity)
        {
            _db.Roles.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(Role entity)
        {
            _db.Roles.Remove(entity);
            _db.SaveChanges();
        }

        public void Update(Role entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public Role FindById(int Id)
        {
            var result = (from r in _db.Roles where r.RoleId == Id select r).FirstOrDefault();
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