using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.Helpers;

namespace Webgentle.Models
{
    public class BookModel
    {
        public int Id { get; set; }
      
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Enter The Author Name")]
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Pagenumber { get; set; }
      

        public int LanguageId { get; set; }
        public string Language { get; set; }
        public int TotalPages { get; set; }
        public DateTime? CreateOn { get; set; }
        public DateTime? UpadateOn { get; set; }

        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }
        public IFormFileCollection GalleryFiles { get; set; }
        public List<GalleryModel> Gallery { get; set; }


    }
}
