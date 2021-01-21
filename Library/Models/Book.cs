using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Desc { get; set; }
        public bool IsFavourite { get; set; }
        public bool IsNew { get; set; }
        public bool IsRecomended { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string Img { get; set; }
        public string File { get; set; }

    }
}
