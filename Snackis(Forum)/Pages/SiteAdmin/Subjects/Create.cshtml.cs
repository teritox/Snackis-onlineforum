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
    public class CreateSubjectModel : PageModel
    {
        private readonly Snackis_Forum_.Data.ForumContext _context;

        public CreateSubjectModel(Snackis_Forum_.Data.ForumContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Subject Subject { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Subjects.Add(Subject);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}
