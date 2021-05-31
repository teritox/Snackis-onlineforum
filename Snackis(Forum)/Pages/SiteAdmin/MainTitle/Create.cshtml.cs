using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Snackis_Forum_.Data;
using Snackis_Forum_.Models;

namespace Snackis_Forum_.Pages.SiteAdmin
{
    public class CreateMainTitleModel : PageModel
    {
        private readonly Snackis_Forum_.Data.ForumContext _context;

        public CreateMainTitleModel(Snackis_Forum_.Data.ForumContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SiteTitle SiteTitle { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SiteTitle.Add(SiteTitle);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}
