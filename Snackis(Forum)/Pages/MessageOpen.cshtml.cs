using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis_Forum_.Data;
using Snackis_Forum_.Models;
using Snackis_Forum_.Services;

namespace Snackis_Forum_.Pages
{
    public class MessageModel : PageModel
    {
        private readonly IForumDataService _ds;
        private ForumContext _ctx;
        public MessageModel(IForumDataService ds, ForumContext ctx)
        {
            _ds = ds;
            _ctx = ctx;
        }

        [BindProperty(SupportsGet = true)]
        public int MessageID { get; set; }

        public PrivateMessage Message { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if(MessageID != 0)
            {
                Message = _ds.GetSinglePrivateMessage(MessageID);

                var message = await _ctx.PrivateMessages.FindAsync(Message.Id);
                message.Read = true;
                await _ctx.SaveChangesAsync();
            }

            return Page();
        }
    }
}
