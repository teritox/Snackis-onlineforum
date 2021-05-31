using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis_Forum_.Models;
using Snackis_Forum_.Services;
using Microsoft.AspNetCore.Identity.UI;
using System.Security.Claims;

namespace Snackis_Forum_.Pages
{
    public class ThreadIndexModel : PageModel
    {
        private readonly IForumDataService _ds;

        public ThreadIndexModel(IForumDataService ds)
        {
            _ds = ds;
        }

        [BindProperty(SupportsGet = true)]
        public int? SubjectId { get; set; }

        public IEnumerable<SubjectThread> ThreadList { get; set; }

        public IEnumerable<Subject> SubjectList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (SubjectId != null)
            {
                SubjectList = await _ds.GetSubjects();
                ThreadList = await _ds.GetForumThreads();

            }


            return Page();
        }
    }
}
