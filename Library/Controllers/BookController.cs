using Library.Models;
using Library.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;

namespace Library.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        BookContext _context;
        private IWebHostEnvironment WebHostEnvironment;
        public BookController(BookContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            WebHostEnvironment = webHostEnvironment;
        }
        public ActionResult Index(int? category, string name)
        {
            IQueryable<Book> books = _context.Books.Include(p => p.Category);
            if (category != null && category != 0)
            {
                books = books.Where(p => p.CategoryId == category);
            }
            if (!String.IsNullOrEmpty(name))
            {
                books = books.Where(p => p.Name.Contains(name));
            }

            List<Category> categories = _context.Categories.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { Name = "All", Id = 0 });

            BookListViewModel viewModel = new BookListViewModel
            {
                Books = books.ToList(),
                Categories = new SelectList(categories, "Id", "Name"),
                Name = name
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Book(int? id)
        {
            if (id != null)
            {
                Book book = await _context.Books.FirstOrDefaultAsync(p => p.Id == id);
                if (book != null)
                    return View(book);
            }
            return NotFound();
        }
        public IActionResult Create()
        {
            // Формируем список команд для передачи в представление
            SelectList categories = new SelectList(_context.Categories, "Id", "Name");
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateBookViewModel vm)

        {
            string stringImgName = UploadImg(vm);
            string stringFileName = UploadFile(vm);
            var book = new Book
            {
                Name = vm.Name,
                Img = stringImgName,
                Desc = vm.Desc,
                File= stringFileName,
                CategoryId = vm.CategoryId,
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private string UploadImg(CreateBookViewModel vm)
        {
            string fileName = null;
            if (vm.Img != null)
            {
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + vm.Img.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.Img.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        private string UploadFile(CreateBookViewModel vm)
        {
            string fileName = null;
            if (vm.File != null)
            {
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "Files");
                fileName = Guid.NewGuid().ToString() + vm.File.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.File.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        /*public IActionResult GetFile()
        {
            string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "Files");
            string file_path = Path.Combine(uploadDir, "hello.pdf");
            string file_type = "application/pdf";
            string file_name = "hello.pdf";
            return File(file_path,file_type,file_name);
        }*/
       

    }
}
