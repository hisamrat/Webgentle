using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.Data;
using Webgentle.Models;

namespace Webgentle.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IConfiguration _configuration;

        public BookRepository(BookStoreContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
         
        public int AddNewBook(BookModel model)
        {
            var newbook = new Books()
            {
                Author=model.Author,
                CreateOn=DateTime.UtcNow,
                Title=model.Title,
                LanguageId = model.LanguageId,
                Description =model.Description,
                TotalPages=model.TotalPages,
                UpadateOn=DateTime.UtcNow,
                CoverImageUrl=model.CoverImageUrl
                
            };

            newbook.BookGallery = new List<BookGallery>();
            foreach(var file in model.Gallery)
            {
                newbook.BookGallery.Add(new BookGallery()
                {
                    Name=file.Name,
                    URL=file.URL
                });
            }
            
            _context.Books.Add(newbook);
            _context.SaveChanges();
            return newbook.Id;
        }
        public List<BookModel> GetAllBooks()
        {
            return  _context.Books
                 .Select(book => new BookModel()
                 {
                     Author = book.Author,
                     Category = book.Category,
                     Description = book.Description,
                     Id = book.Id,
                     LanguageId = book.LanguageId,
                     Language = book.Language.Name,
                     Title = book.Title,
                     TotalPages = book.TotalPages,
                     CoverImageUrl=book.CoverImageUrl
                    
                 }).ToList();
        }
        public BookModel GetBookById(int id)
        {
            
            var books= _context.Books.Where(x=>x.Id==id).Select(book=>new BookModel()
            { 

                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl=book.CoverImageUrl,
                    Gallery=book.BookGallery.Select(g=>new GalleryModel()
                    {
                        Id=g.Id,
                        Name=g.Name,
                        URL=g.URL
                    }).ToList()
                }).FirstOrDefault();
           
            
           

            return books;


            }
           
            
        
        
        public Books DeleteBookById(int id)
        {
           

            var data = _context.Books.Find(id);
            _context.Books.Remove(data);
            _context.SaveChanges();
            return data;
        }
        public BookModel EditBookById(BookModel model)
        {
            var newbook = new Books()
            {
                Author = model.Author,
                CreateOn = DateTime.UtcNow,
                Title = model.Title,
                LanguageId = model.LanguageId,
               Id=model.Id,
                Description = model.Description,
                TotalPages = model.TotalPages,
                UpadateOn = DateTime.UtcNow

            };

            _context.Books.Update(newbook);
            _context.SaveChanges();
            return model;
        }
       

        public List<BookModel> SearchBook(string title,string authorName)
        {
            return null;
        }
        
        //public string GetAppName()
        //{
        //    return _configuration["AppName"];
        //}
    }
}
