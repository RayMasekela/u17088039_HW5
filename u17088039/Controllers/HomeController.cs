using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using u17088039.Models; 

namespace u17088039.Controllers
{
    public class HomeController : Controller
    {
        LibraryService Service = new LibraryService();
        public ActionResult Index()
        {
          
            IndexVM indexVM = new IndexVM();
            List<Book> Books = Service.GetBooks();
           

            indexVM.Books = Books;
            indexVM.Authors = Service.GetAuthors();
            indexVM.Types = Service.GetTypes();

            return View(indexVM);
        }
        [HttpPost]
        public ActionResult SearchStudent(int bookId, string studentName = null, string StudentClass = null)
        {
            StudentVM studentVM = new StudentVM();
            List<Borrow> bb = Service.GetBorrowsById(bookId);

            studentVM.Book = Service.GetBooks().Where(b => b.BookId.Equals(bookId)).FirstOrDefault();
            List<Student> students = Service.GetStudents();
            List<Borrow> Borrows = Service.GetBorrowsById(bookId);
            foreach (Student student in students)
            {
                foreach (Borrow borrow in Borrows)
                {
                    if (borrow.Student.StudentId == student.StudentId && borrow.BroughtDate == "")
                    {
                        student.HasBook = true;
                    }
                    else
                    {
                        student.HasBook = false;
                    }
                }
            }
            List<StudentClass> classes = new List<StudentClass>();
            foreach (Student student in Service.GetStudents())
            {
                StudentClass cl = new StudentClass
                {
                    Name = student.Class
                };

                if (classes.Where(n => n.Name == student.Class).Count() == 0)
                {
                    classes.Add(cl);
                }
            }

            if (studentName != "")
            {
                studentVM.Students = Service.GetStudents().Where(cl => cl.Name.Contains(studentName)).ToList();
            }
            if (StudentClass != "Select a Class")
            {
                studentVM.Students = Service.GetStudents().Where(cl => cl.Class == StudentClass).ToList();
            }
            studentVM.StudentClasses = classes;
            return View( "ViewStudent",studentVM);
        }

            [HttpPost]
        public ActionResult SearchBook(string bookName = null, string author = null, string typeName = null)
        {
            IndexVM indexVM = new IndexVM();

            if (typeName != "Select a Type")
            {
                indexVM.Books = Service.GetBooks().Where(book => book.Type == typeName).ToList();
            }
            if (bookName != "")
            {
                indexVM.Books = Service.GetBooks().Where(book => book.Name.Contains(bookName.Trim())).ToList();
            }
            if (author != "Select a Author")
            {
                indexVM.Books = Service.GetBooks().Where(book => book.Author.Contains(author.Trim())).ToList();
            }


            indexVM.Types = Service.GetTypes();
            indexVM.Authors = Service.GetAuthors();
            return View("Index", indexVM);
        }

            public ActionResult ViewBorrow(int bookId)
        {
           
            BorrowsVM borrowsVM = new BorrowsVM();
            borrowsVM.Book = Service.GetBooks().Where(b => b.BookId.Equals(bookId)).FirstOrDefault();
            borrowsVM.Borrows = Service.GetBorrowsById(bookId);
            return View(borrowsVM);
        }

        public ActionResult ViewStudent(int bookId)
        {
            StudentVM studentVM = new StudentVM();
            List<Borrow> bb = Service.GetBorrowsById(bookId);

            studentVM.Book = Service.GetBooks().Where(b => b.BookId.Equals(bookId)).FirstOrDefault();
            List<Student> students = Service.GetStudents();
            List<Borrow> Borrows = Service.GetBorrowsById(bookId);
            foreach (Student student in students)
            {
                foreach(Borrow borrow in Borrows)
                {
                    if(borrow.Student.StudentId == student.StudentId && borrow.BroughtDate == "")
                    {
                        student.HasBook = true;
                    }
                    else
                    {
                        student.HasBook = false;
                    }                
                }
            }
            List<StudentClass> classes = new List<StudentClass>();
            foreach (Student student in Service.GetStudents())
            {
                StudentClass cl = new StudentClass
                {
                    Name = student.Class
                };

                if (classes.Where(n => n.Name == student.Class).Count() == 0)
                {
                    classes.Add(cl);
                }
            }

            studentVM.Students = students;
            studentVM.StudentClasses = classes;
            return View(studentVM);
        }
    }
}