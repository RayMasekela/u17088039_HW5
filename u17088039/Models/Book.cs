using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u17088039.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public int PageCount { get; set; }
        public int Points { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public bool IsBorrowed { get; set; }
        public string Status { get; set; }
    }
}