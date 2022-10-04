using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u17088039.Models
{
    public class Borrow
    {
        public int BorrowId { get; set; }
        public string TakenDate { get; set; }
        public string BroughtDate { get; set; }
        public Student Student { get; set; }
        public int BookId { get; set; }
    }
}