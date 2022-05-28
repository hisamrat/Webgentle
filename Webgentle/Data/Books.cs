using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webgentle.Data
{
    public class Books
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter The Title")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Please Enter The Author Name")]
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Pagenumber { get; set; }
        public int LanguageId { get; set; }
    
        public int TotalPages { get; set; }
        public DateTime? CreateOn { get; set; }
        public DateTime? UpadateOn { get; set; }
        public Language Language { get; set; }

        public string CoverImageUrl { get; set; }
        public ICollection<BookGallery> BookGallery { get; set; }

    }
}
