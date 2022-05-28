using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.Data;
using Webgentle.Models;
using Webgentle.Repository;

namespace Webgentle.Controllers
{
    public class BookController : Controller
    {

        private readonly BookRepository _bookRepository = null;
        private readonly LanguageRepository _languageRepository = null;

        private readonly IWebHostEnvironment _webHostEnvironment;
        

        public BookController(BookRepository bookRepository, LanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;


        }
        
        
        public ViewResult GetAllBooks()
        {
            ViewBag.language = new SelectList(_languageRepository.GetLanguages(), "Id", "Name");
            var data= _bookRepository.GetAllBooks();
            return View(data);

        }

        public ViewResult GetBook(int id)
        {
            var data= _bookRepository.GetBookById(id);
            
            return View(data);
        }
        public ViewResult Delete(int id)
        {
            ViewBag.language = new SelectList(_languageRepository.GetLanguages(), "Id", "Name");
            var data = _bookRepository.GetBookById(id);

            return View(data);
        }
        public IActionResult DeleteBook(int id)
        {
            ViewBag.language = new SelectList(_languageRepository.GetLanguages(), "Id", "Name");
            _bookRepository.DeleteBookById(id);

            return RedirectToAction("GetAllBooks");
        }
        
        public ViewResult Edit(int id)
        {
            ViewBag.language = new SelectList(_languageRepository.GetLanguages(), "Id", "Name");
            var data = _bookRepository.GetBookById(id);

            return View(data);
        }
        public IActionResult EditBook(BookModel books)
        {
            ViewBag.language = new SelectList(_languageRepository.GetLanguages(), "Id", "Name");
            _bookRepository.EditBookById(books);

            return RedirectToAction("GetAllBooks");
        }
        public List<BookModel> SearchBooks(string bookName,string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }
        [Authorize]
        [HttpGet]
        public ViewResult Addnewbook(bool isSuccess = false,int bookid=0)
        {


            ViewBag.language =new SelectList( _languageRepository.GetLanguages(),"Id","Name");
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookid;
            return View();
        }
        [HttpPost]
        public IActionResult Addnewbook(BookModel model)
        {
            if(model.CoverPhoto !=null)
            {
                string folder = "books/cover/";
               model.CoverImageUrl= UploadImage(folder,model.CoverPhoto);
                
            }
            
            if (model.GalleryFiles != null)
            {
                string folder = "books/gallery/";
                model.Gallery = new List<GalleryModel>();
                foreach(var file in model.GalleryFiles)
                {
                    var gallery = new GalleryModel()
                    {
                        Name = file.FileName,
                        URL = UploadImage(folder, file)
                    };
                    model.Gallery.Add(gallery);
                }
               

            }

            if (ModelState.IsValid)
            {
                int id = _bookRepository.AddNewBook(model);
                if (id > 0)
                {
                    return RedirectToAction(nameof(Addnewbook), new { isSuccess = true, bookid = id });
                }
            }

            ViewBag.language = new SelectList(_languageRepository.GetLanguages(), "Id", "Name");
            return View();
        }

        private string UploadImage(string folderPath,IFormFile file)
        {
           
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
           
            string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            file.CopyTo(new FileStream(serverfolder, FileMode.Create));
            return "/" + folderPath;
        }

        

       

    }
}
