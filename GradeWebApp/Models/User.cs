using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using GradeWebApp.Repository;

namespace GradeWebApp.Models
{
    public partial class User : IEntity
    {
        public int UserId { get; set; }

        [Required]
        public String Username { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [DataType(DataType.Password)]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "*Passwords doesn't match")]
        public String RepeatPassword { get; set; }
        //public Role RoleId { get; set; }
        [Required]
        public String FirstName { get; set; }

        [Required]
        public String LastName { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public int LoginAttempt { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? LockedDate { get; set; }



        public virtual ICollection<Role> Roles { get; set; }
    }
}