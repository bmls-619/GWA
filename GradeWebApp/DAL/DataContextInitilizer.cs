using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using GradeWebApp.Models;

namespace GradeWebApp.DAL
{
    public class DataContextInitilizer : DropCreateDatabaseAlways<GContext>
    {
        protected override void Seed(GContext context)
        {

            Role role1 = new Role { RoleName = "Admin" };
            Role role2 = new Role { RoleName = "User" };

            User user1 = new User { Username = "admin", Email = "admin@gwa.com", FirstName = "Admin", LastName = "Admin", Password = "123456", RepeatPassword = "123456", IsActive = true, CreateDate = DateTime.UtcNow, Roles = new List<Role>() };

            User user2 = new User { Username = "user", Email = "user@gwa.com", FirstName = "User", LastName = "User", Password = "123456", RepeatPassword = "123456", IsActive = true, CreateDate = DateTime.UtcNow, Roles = new List<Role>() };

            user1.Roles.Add(role1);
            user2.Roles.Add(role2);

            context.Users.Add(user1);
            context.Users.Add(user2);

            Book book1 = new Book { Book_Name = "Rosetta Stone British English Workbook Level 3".ToUpper(), ISBN10 = "1603919554", ISBN13 = "9781603919555", Pages = 250, Publisher = "Fairfield Language Technology".ToUpper(), Language = "Other".ToUpper() };
            Book book2 = new Book { Book_Name = " AP English Literature & Composition (For Dummies, 2008)".ToUpper(), ISBN10 = "0470194251", ISBN13 = "9780470194256", Pages = 384, Publisher = "For Dummies".ToUpper(), Language = "English".ToUpper() };

            context.Books.Add(book1);
            context.Books.Add(book2);

            Chapter chapter = new Chapter { BookID = 1, ChapterDescription = "CHAPTER 1" };
            Chapter chapter2 = new Chapter { BookID = 1, ChapterDescription = "CHAPTER 2" };
            Chapter chapter3 = new Chapter { BookID = 2, ChapterDescription = "CHAPTER 1" };

            context.Chapters.Add(chapter);
            context.Chapters.Add(chapter2);
            context.Chapters.Add(chapter3);

            Student student = new Student { Name = "RONEL", LastName = "MENDEZ MORENO", Level = 9 };
            Student student2 = new Student { Name = "KEN", LastName = "KENNEDY", Level = 5 };
            Student student3 = new Student { Name = "JOHN", LastName = "CONNOR", Level = 5 };

            context.Students.Add(student);
            context.Students.Add(student2);
            context.Students.Add(student3);

            context.SaveChanges();


            Grade grade = new Grade { Student_ID = student.StudentId, Book_ID = 1, Chapter_ID = 2, Homework = 2.65521, Excercise = 2.26551, Participation = 60.5, CreateDate = DateTime.Now };
            Grade grade2 = new Grade { Student_ID = student2.StudentId, Book_ID = 1, Chapter_ID = 1, Homework = 10.5633, Excercise = 2.6534, Participation = 5.6348, CreateDate = DateTime.Now };
            //Grade grade3 = new Grade { Student_ID = student3.StudentId, Book_ID = 1, Chapter_ID = 1, Homework = 10.5633, Excercise = 2.6534, Participation = 5.6348, CreateDate = DateTime.Now };

            context.Grades.Add(grade);
            context.Grades.Add(grade2);
            //context.Grades.Add(grade3);


            context.SaveChanges();
        }
    }


}