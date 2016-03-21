using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GradeWebApp.Repository;
using System.ComponentModel.DataAnnotations;

namespace GradeWebApp.Models
{
    public partial class Grade : IEntity
    {

        private string fullName;
        private double totalGrade = 0;


        [Required]
        public int GradeId { get; set; }

        [Required]
        public int Student_ID { get; set; }

        string Fullname
        {
            get
            {
                return fullName = student.Name + " " + student.LastName;
            }
        }

        [Required]
        public int Book_ID { get; set; }

        [Required]
        public int Chapter_ID { get; set; }

        public double Homework { get; set; }
        public double Excercise { get; set; }
        public double Participation { get; set; }
        public double Total
        {
            get
            {
               return totalGrade = Homework + Excercise + Participation;
            }
        }

        public DateTime CreateDate { get; set; }

        public virtual Student student { get; set; }
        public virtual Book book { get; set; }
        public virtual Chapter chapter { get; set; }
    }
}