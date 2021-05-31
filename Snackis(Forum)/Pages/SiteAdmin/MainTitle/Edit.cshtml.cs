using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Snackis_Forum_.Data;
using Snackis_Forum_.Models;

namespace Snackis_Forum_.Pages.SiteAdmin
{
    public class EditMainTitleModel : PageModel
    {
        private readonly Snackis_Forum_.Data.ForumContext _context;

        public EditMainTitleModel(Snackis_Forum_.Data.ForumContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SiteTitle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteTitleExists(SiteTitle.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Index");
        }

        private bool SiteTitleExists(int id)
        {
            return _context.SiteTitle.Any(e => e.Id == id);
        }
    }
}
