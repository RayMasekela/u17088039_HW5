using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace u17088039.Models
{
    public class LibraryService
    {
        private String ConnectionString;
        public LibraryService()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public List<Author> GetAuthors()
        {
            List<Author> authors = new List<Author>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select * from authors ", con))
                {
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            Author author = new Author
                            {
                                AuthorId = (int)r["authorId"],
                                Name = r["Name"].ToString(),
                                Surname = r["surname"].ToString()
                            };
                            authors.Add(author);
                        }
                    }
                }
                con.Close();
            }
            return authors;
        }

        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();    
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(" select books.bookId, books.name as bookname, books.pagecount, books.point, authors.name, types.name  as type " +
                    "  from books  inner join authors on books.authorId = authors.authorId " +
                    " inner join types on types.typeId = books.typeId ", conn))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Book book = new Book
                            {
                                BookId = (int)rdr["bookId"],
                                Name = rdr["bookname"].ToString(),
                                PageCount = (int)rdr["pagecount"],
                                Points = (int)rdr["point"], 
                                Author = (string)rdr["name"],
                                Type = (string)rdr["type"],
                            };
                            books.Add(book);
                        }
                    }
                }                              
                conn.Close();
            }

            foreach (var book in books)
            {               
                List<Borrow> borrowedBooks = GetBorrowsById(book.BookId);      
                if (borrowedBooks.Where(b => b.BroughtDate == "").Count() > 0)
                {
                    book.Status = "Book Out";
                }
                else
                {
                    book.Status = "Available";
                }
            }
            return books;
        }

        public List<BookType> GetTypes()
        {
            List<BookType> types = new List<BookType>();
            using(SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using(SqlCommand command = new SqlCommand(" select * from types ", con))
                {
                    using(SqlDataReader rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            BookType type = new BookType
                            {
                                TypeId = (int)rdr["typeId"],
                                Name = (string)rdr["name"]
                            };
                            types.Add(type);
                        }
                    }
                }
                con.Close();
            }
            return types;
        }

        public List<Borrow> GetBorrows()
        {
            List<Borrow> Borrows = new List<Borrow>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("select borrowId, studentId,bookId, takenDate, broughtDate from borrows"
                 , con))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Borrow Borrow = new Borrow
                            {
                                BorrowId = (int)reader["borrowId"],
                                Student = GetStudents().Where( st => st.StudentId == (int)reader["studentId"]).FirstOrDefault(),
                                TakenDate = reader["takenDate"].ToString(),
                                BroughtDate = (string)reader["broughtDate"].ToString(),
                                BookId = (int)reader["bookId"]                               
                            };
                            Borrows.Add(Borrow);
                        }                       
                    }   
                }
                con.Close();
            }
            return Borrows;
        }

        public List<Borrow> GetBorrowsById(int bookId)
        {
            List<Borrow> Borrows = new List<Borrow>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("select borrowId, studentId,bookId, takenDate, broughtDate from borrows where bookId = " + bookId
                 , con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Borrow Borrow = new Borrow
                            {
                                BorrowId = (int)reader["borrowId"],
                                Student = GetStudents().Where(st => st.StudentId == (int)reader["studentId"]).FirstOrDefault(),
                                TakenDate = reader["takenDate"].ToString(),
                                BroughtDate = (string)reader["broughtDate"].ToString(),
                                BookId = (int)reader["bookId"]
                            };

                            Borrows.Add(Borrow);
                        }
                    }
                }
                con.Close();
            }
            return Borrows;
        }


        public List<Student> GetStudents()
        {
            List<Student> Students = new List<Student>();
            using(SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("select * from students", con))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                StudentId = (int)reader["studentId"],
                                Points = (int)reader["point"],
                                Class = (string)reader["class"]
                            };
                            Students.Add(student);
                        }
                    }
                }
                con.Close();
            }
            return Students;
        }


            

    }
}