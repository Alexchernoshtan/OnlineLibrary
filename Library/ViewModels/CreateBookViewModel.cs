using Library.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.ViewModels
{
    public class CreateBookViewModel
    {
        public string Name { get; set; }

        public string Desc { get; set; }
        public bool IsFavourite { get; set; }
        public bool IsNew { get; set; }
        public bool IsRecomended { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public IFormFile Img { get; set; }
        public IFormFile File { get; set; }

    }
}
