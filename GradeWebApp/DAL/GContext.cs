using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Security;
using GradeWebApp.Models;


namespace GradeWebApp.DAL
{
    public class GContext : DbContext
    {
        public GContext() : base("DefaultConnection") { }

        //static GContext()
        //{
        //    Database.SetInitializer<GContext>(new DropCreateDatabaseAlways<GContext>());
        //    GContext gContext = new GContext();

        //    Role role1 = new Role { RoleName = "Admin"};
        //    Role role2 = new Role { RoleName = "User"};

        //    User user1 = new User { Username = "admin", Email = "admin@gwa.com", FirstName = "Admin", LastName = "Admin", Password = "123456", RepeatPassword = "123456", IsActive = true, CreateDate = DateTime.UtcNow, Roles = new List<Role>() };

        //    User user2 = new User { Username = "user", Email = "user@gwa.com", FirstName = "User", LastName = "User", Password = "123456", RepeatPassword = "123456", IsActive = true, CreateDate = DateTime.UtcNow, Roles = new List<Role>() };

        //    user1.Roles.Add(role1);
        //    user2.Roles.Add(role2);

        //    gContext.Users.Add(user1);
        //    gContext.Users.Add(user2);

        //    Book book1 = new Book { Book_Name = "Rosetta Stone British English Workbook Level 3".ToUpper(), ISBN10 = "1603919554", ISBN13 = "9781603919555", Pages = 250, Publisher ="Fairfield Language Technology".ToUpper(), Language ="Other".ToUpper()};
        //    Book book2 = new Book { Book_Name = " AP English Literature & Composition (For Dummies, 2008)".ToUpper(), ISBN10 = "0470194251", ISBN13 = "9780470194256", Pages = 384, Publisher = "For Dummies".ToUpper(), Language = "English".ToUpper()};

        //    gContext.Books.Add(book1);
        //    gContext.Books.Add(book2);

        //    Chapter chapter = new Chapter {BookID = 1, ChapterDescription = "CHAPTER 1" };
        //    Chapter chapter2 = new Chapter { BookID = 1, ChapterDescription = "CHAPTER 2" };
        //    Chapter chapter3 = new Chapter { BookID = 2, ChapterDescription = "CHAPTER 1" };

        //    gContext.Chapters.Add(chapter);
        //    gContext.Chapters.Add(chapter2);
        //    gContext.Chapters.Add(chapter3);

        //    Student student = new Student { Name = "RONEL", LastName ="MENDEZ MORENO", Level = 9 };
        //    Student student2 = new Student { Name = "KEN", LastName = "KENNEDY", Level = 5 };
        //    Student student3 = new Student { Name = "JOHN", LastName = "CONNOR", Level = 5 };

        //    gContext.Students.Add(student);
        //    gContext.Students.Add(student2);
        //    gContext.Students.Add(student3);

        //    gContext.SaveChanges();


        //    Grade grade = new Grade { Student_ID = student.StudentId, Book_ID = 1, Chapter_ID = 2, Homework = 2.65521, Excercise = 2.26551, Participation = 60.5, CreateDate = DateTime.Now };
        //    Grade grade2 = new Grade { Student_ID = student2.StudentId, Book_ID = 1, Chapter_ID = 1, Homework = 10.5633, Excercise = 2.6534, Participation = 5.6348, CreateDate = DateTime.Now };
        //    //Grade grade3 = new Grade { Student_ID = student3.StudentId, Book_ID = 1, Chapter_ID = 1, Homework = 10.5633, Excercise = 2.6534, Participation = 5.6348, CreateDate = DateTime.Now };

        //    gContext.Grades.Add(grade);
        //    gContext.Grades.Add(grade2);
        //    //gContext.Grades.Add(grade3);


        //    gContext.SaveChanges();
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                        .Property(b => b.BookId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Chapter>()
                        .Property(c => c.ChapterID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Grade>()
                        .Property(g => g.GradeId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            modelBuilder.Entity<Log>()
                        .Property(l => l.LogId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            modelBuilder.Entity<Student>()
                        .Property(s => s.StudentId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            modelBuilder.Entity<User>()
                        .Property(u => u.UserId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            modelBuilder.Entity<Role>()
                        .Property(r => r.RoleId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            //Entity relationships
            modelBuilder.Entity<Grade>()
                        .HasRequired<Book>(g => g.book)
                        .WithMany(b => b.BookList).HasForeignKey(g => g.Book_ID);

            modelBuilder.Entity<Grade>()
                        .HasRequired<Student>(g => g.student)
                        .WithMany(s => s.StudentList).HasForeignKey(g => g.Student_ID);

            modelBuilder.Entity<Grade>()
                        .HasRequired<Chapter>(b => b.chapter)
                        .WithMany(c => c.ChapterList).HasForeignKey(b => b.Chapter_ID);

            modelBuilder.Entity<Chapter>()
                        .HasRequired<Book>(b => b.book)
                        .WithMany(b => b.ChapterList).HasForeignKey(b => b.BookID).WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.Roles)
                        .WithMany(r => r.Users)
                        .Map(m =>
                                {
                                    m.ToTable("UserRoles"); m.MapLeftKey("UserId"); m.MapRightKey("RoleId");
                                });

        }


        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Chapter> Chapters { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

    }
}