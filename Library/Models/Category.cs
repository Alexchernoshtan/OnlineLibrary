using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
        public Category()
        {
            Books = new List<Book>();
        }
    }
}
