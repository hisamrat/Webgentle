using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.Data;
using Webgentle.Models;

namespace Webgentle.Repository
{
   
    public class LanguageRepository
    {
        private readonly BookStoreContext _context;

        public LanguageRepository(BookStoreContext context)
        {
            _context = context;
        }
         public List<LanguageModel> GetLanguages()
        {
            return _context.Language.Select(x => new LanguageModel()
            {
                Id=x.Id,
                Description=x.Description,
                Name=x.Name
            }).ToList();
        }
        
    }
}
