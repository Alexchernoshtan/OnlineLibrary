using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Library.Controllers
{
    
    public class HomeController : Controller
    {
        BookContext db;
        public HomeController(BookContext context)
        {
            db = context;
        }
        public ActionResult Index(int? company, string name)
        {
            IQueryable<Book> users = db.Books.Include(p => p.Category);
            if (company != null && company != 0)
            {
                users = users.Where(p => p.CategoryId == company);
            }
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name.Contains(name));
            }

            List<Category> categories = db.Categories.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { Name = "Все", Id = 0 });

            BookListViewModel viewModel = new BookListViewModel
            {
                Books = users.ToList(),
                Categories = new SelectList(categories, "Id", "Name"),
                Name = name
            };
            return View(viewModel);
        }


    }
}
