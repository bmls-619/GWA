using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GradeWebApp.Models
{
    public class Log
    {
        [Key]
        public int LogId { get; set; }
        public int User_Id { get; set; }
        public string MetodoUsado { get; set; }
        public string Suceso { get; set; }
        public DateTime CreateDate { get; set; }

        //public virtual User user { get; set; }
    }
}