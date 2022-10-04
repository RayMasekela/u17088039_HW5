using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u17088039.Models
{
    public class BorrowsVM
    {
        public Book Book { get; set; }
        public List<Borrow> Borrows { get; set; }
    }
}