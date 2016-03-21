using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GradeWebApp.Repository;

namespace GradeWebApp.Models
{
    public partial class Book : IEntity
    {
        public Book()
        {
            BookList = new List<Grade>();
            ChapterList = new List<Chapter>();
        }

         [Required]
        public int BookId { get; set; }

        [Required]
        [Display(Name="Book Name")]
        public string Book_Name { get; set; }

        [Required]
        public string ISBN10 { get; set; }
        public string ISBN13 { get; set; }
        public int Pages { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public virtual ICollection<Grade> BookList { get; set; }
        public virtual ICollection<Chapter> ChapterList { get; set; }
    }
}