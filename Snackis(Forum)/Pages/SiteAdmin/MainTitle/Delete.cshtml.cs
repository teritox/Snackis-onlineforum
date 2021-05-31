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
    public class DeleteMainTitleModel : PageModel
    {
        private readonly Snackis_Forum_.Data.ForumContext _context;

        public DeleteMainTitleModel(Snackis_Forum_.Data.ForumContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SiteTitle SiteTitle { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SiteTitle = await _context.SiteTitle.FirstOrDefaultAsync(m => m.Id == id);

            if (SiteTitle == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SiteTitle = await _context.SiteTitle.FindAsync(id);

            if (SiteTitle != null)
            {
                _context.SiteTitle.Remove(SiteTitle);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Index");
        }
    }
}
