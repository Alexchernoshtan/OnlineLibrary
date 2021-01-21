using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public SelectList Categories { get; set; }
        public string Name { get; set; }
    }
}
