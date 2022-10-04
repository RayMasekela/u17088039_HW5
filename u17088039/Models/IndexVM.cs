using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u17088039.Models
{
    public class IndexVM
    {
        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
        public List<BookType> Types { get; set; }
    }
}