using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis_Forum_.Data;
using Snackis_Forum_.Models;
using Snackis_Forum_.Services;

namespace Snackis_Forum_.Pages
{
    public class PostsModel : PageModel
    {
        private readonly IForumDataService _ds;
        private readonly ForumContext _ctx;
        private readonly IFulaOrdGateway _fulaord;

        public PostsModel(IForumDataService ds, ForumContext ctx, IFulaOrdGateway fulaord)
        {
            _ds = ds;
            _ctx = ctx;
            _fulaord = fulaord;
        }

        [BindProperty(SupportsGet = true)]
        public int? ThreadId { get; set; }

        public IEnumerable<SubjectThread> ThreadList { get; set; }
        public IEnumerable<Post> PostList { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PostText { get; set; }

        [BindProperty(SupportsGet = true)]
        public int AnswerTo { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Reported { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (ThreadId != null)
            {
                ThreadList = await _ds.GetForumThreads();

                PostList = await _ds.GetForumPosts((int)ThreadId);

            }


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (PostText != "" && PostText != null)
            {

                var userId = "";

                if(User.FindFirstValue(ClaimTypes.NameIdentifier) != null)
                {
                    userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                }

                PostText = await _fulaord.GetFilteredItem(PostText);

                Post post = new Post
                {
                    Text = PostText,
                    Author = userId,
                    PostDate = DateTime.Now,
                    Reported = false,
                    ThreadId = (int)ThreadId,
                    AnswerToId = AnswerTo
                };

                _ctx.Posts.Add(post);
                await _ctx.SaveChangesAsync();

            }

                
            if (Reported != 0)
            {
                var post = await _ctx.Posts.FindAsync(Reported);

                post.Reported = true;

                await _ctx.SaveChangesAsync();
            }

            return RedirectToPage("./Posts", new { threadid = ThreadId });
        }
    }
}
