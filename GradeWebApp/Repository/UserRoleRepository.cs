//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Data;
//using GradeWebApp.Models;
//using GradeWebApp.Repository;
//using GradeWebApp.DAL;

//namespace GradeWebApp.Repository
//{
//    public class UserRoleRepository : IRepository<UserRole>
//    {
//        private GContext _db;
//        public UserRoleRepository(GContext db)
//        {
//            this._db = db;
//        }

//        public IEnumerable<UserRole> List
//        {
//            get
//            {
//                return _db.UserRoles.ToList();
//            }
//        }

//        public void Add(UserRole entity)
//        {
//            _db.UserRoles.Add(entity);
//            _db.SaveChanges();
//        }

//        public void Delete(UserRole entity)
//        {
//            _db.UserRoles.Remove(entity);
//            _db.SaveChanges();
//        }

//        public void Update(UserRole entity)
//        {
//            _db.Entry(entity).State = EntityState.Modified;
//            _db.SaveChanges();
//        }

//        public UserRole FindById(int Id)
//        {
//            var result = (from r in _db.UserRoles where r.UserId == Id select r).FirstOrDefault();
//            return result;
//        }

//        private bool disposed = false;

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!this.disposed)
//            {
//                if (disposing)
//                {
//                    _db.Dispose();
//                }
//            }
//            this.disposed = true;
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//    }
//}