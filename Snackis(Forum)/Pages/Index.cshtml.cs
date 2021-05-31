using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Snackis_Forum_.Models;
using Snackis_Forum_.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis_Forum_.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IForumDataService _ds;

        public SiteTitle SiteTitle { get; set; }
        public IEnumerable<Subject> SubjectList { get; set; }
        public IEnumerable<SubjectThread> ThreadList { get; set; }

        public IndexModel(IForumDataService ds)
        {
            _ds = ds;
        }

        public async Task<IActionResult> OnGetAsync()
        {

            SiteTitle = await _ds.GetSiteTitle();

            SubjectList = await _ds.GetSubjects();

            ThreadList = await _ds.GetForumThreads();

            return Page();
        }
    }
}

