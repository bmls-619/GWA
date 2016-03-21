using GradeWebApp.DAL.Security;
using GradeWebApp.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradeWebApp.Models;
using GradeWebApp.Repository;

namespace GradeWebApp.Controllers
{
    public class HomeController : BaseController
    {

        private GradeRepository gradeRepository;
        public HomeController()
        {
            this.gradeRepository = new GradeRepository(new GContext());
        }

        //
        // GET: /Home/
        [CustomAuthorize(RolesConfigKey = "RolesConfigKey")]
        [CustomAuthorize(UsersConfigKey = "UsersConfigKey")]
        [CustomAuthorize(Roles = "Admin,User")]
        [CustomAuthorize(Users = "1,2")]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GradeChart()
        {
            var gChart = from grd in gradeRepository.List select new {student = grd.student.Fullname, gradeTotal = grd.Total };

            List<string> student = new List<string>();
            List<double> grade = new List<double>();

            foreach(var gc in gChart)
            {
                student.Add(gc.student);
                grade.Add(gc.gradeTotal);
            }

            return Json(new {grades = grade, student = student},0);
        }
    }
}
