using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis_Forum_.Data;
using Snackis_Forum_.Models;
using Snackis_Forum_.Services;

namespace Snackis_Forum_.Pages.SiteAdmin
{
    public class ReportedPostsModel : PageModel
    {
        private readonly ForumContext _ctx;
        private readonly IForumDataService _ds;

        public ReportedPostsModel(ForumContext ctx, IForumDataService ds)
        {
            _ctx = ctx;
            _ds = ds;
        }

        public IEnumerable<Post> PostList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int DeletePostId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int RestorePostId { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if(RestorePostId != 0)
            {
                var post = await _ctx.Posts.FindAsync(RestorePostId);
                post.Reported = false;

                await _ctx.SaveChangesAsync();          
            }

            if(DeletePostId != 0)
            {
                var post = await _ctx.Posts.FindAsync(DeletePostId);

                _ctx.Posts.Remove(post);
                await _ctx.SaveChangesAsync();
            }

            PostList = await _ds.GetReportedPosts();

            return Page();
        }
    }
}
