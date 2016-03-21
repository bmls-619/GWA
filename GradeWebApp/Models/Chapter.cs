using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GradeWebApp.Repository;

namespace GradeWebApp.Models
{
    public class Chapter : IEntity
    {
        public Chapter()
        {
            ChapterList = new List<Grade>();
        }

        [Required]
        public int ChapterID { get; set; }

        [Required]
        public int BookID { get; set; }

        [Required]
        [Display(Name ="Chapter:")]
        public string ChapterDescription { get; set; }

        public virtual ICollection<Grade> ChapterList { get; set; }
        public virtual Book book { get; set; }
    }
}