using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis_Forum_.Data;
using Snackis_Forum_.Models;

namespace Snackis_Forum_.Pages.SiteAdmin
{
    public class IndexModel : PageModel
    {
        private readonly Snackis_Forum_.Data.ForumContext _context;

        public IndexModel(Snackis_Forum_.Data.ForumContext context)
        {
            _context = context;
        }

        public IList<SiteTitle> SiteTitle { get;set; }
        public IList<Subject> Subjects { get; set; }

        public async Task OnGetAsync()
        {
            SiteTitle = await _context.SiteTitle.ToListAsync();
            Subjects = await _context.Subjects.ToListAsync();
        }
    }
}
