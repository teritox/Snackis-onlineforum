using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis_Forum_.Data;
using Snackis_Forum_.Models;

namespace Snackis_Forum_.Pages
{
    public class NewThreadModel : PageModel
    {
        private readonly ForumContext _ctx;

        public NewThreadModel(ForumContext ctx)
        {
            _ctx = ctx;
        }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string PostText { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SubjectID { get; set; }



        [BindProperty(SupportsGet = true)]
        public bool IsFilled { get; set; } = true;



        public async Task<IActionResult> OnPostAsync()
        {
            if (Title != "" && PostText != "")
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                SubjectThread thread = new SubjectThread
                {
                    TreadTitle = Title,
                    CreationDate = DateTime.Now,
                    LatestPost = DateTime.Now,
                    AuthorId = userId,
                    SubjectId = SubjectID
                };

                _ctx.SubjectThreads.Add(thread);
                await _ctx.SaveChangesAsync();

                var newlyAddedSubjectThread = await _ctx.SubjectThreads
                    .Where(t => t.TreadTitle == thread.TreadTitle && t.AuthorId == thread.AuthorId && t.CreationDate == thread.CreationDate).FirstOrDefaultAsync();

                Post post = new Post
                {
                    Text = PostText,
                    Author = userId,
                    PostDate = DateTime.Now,
                    Reported = false,
                    ThreadId = newlyAddedSubjectThread.Id,
                    AnswerToId = 0
                };


                _ctx.Posts.Add(post);
                await _ctx.SaveChangesAsync();

                return RedirectToPage("./Posts", new { threadid = thread.Id });

            }
            else
            {

                IsFilled = false;

                return Page();
            }



        }
    }
}
