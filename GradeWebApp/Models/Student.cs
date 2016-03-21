using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GradeWebApp.Repository;
using System.ComponentModel.DataAnnotations;

namespace GradeWebApp.Models
{
    public partial class Student : IEntity
    {
        public Student()
        {
            StudentList = new List<Grade>();
        }

        public int StudentId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Fullname
        {
            get
            {
                return Name + " " +LastName;
            }
        }

        [Required]
        public int Level { get; set; }

        public virtual ICollection<Grade> StudentList { get; set; }
    }
}