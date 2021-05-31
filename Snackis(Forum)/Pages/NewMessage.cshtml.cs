using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis_Forum_.Areas.Identity.Data;
using Snackis_Forum_.Data;
using Snackis_Forum_.Models;

namespace Snackis_Forum_.Pages
{
    public class NewMessageModel : PageModel
    {
        private readonly ForumContext _ctx;

        public UserManager<ForumUser> _userManager;

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string TextMessage { get; set; }

        [BindProperty]
        public string Receiver { get; set; }


        public NewMessageModel(ForumContext ctx, UserManager<ForumUser> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (Title != null & TextMessage != null)
            {
                var receiver = _userManager.Users.Where(u => u.NickName.ToLower() == Receiver.ToLower()).FirstOrDefault();
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (receiver != null)
                {
                    PrivateMessage message = new PrivateMessage
                    {
                        MessageTitle = Title,
                        Message = TextMessage,
                        MessageSent = DateTime.Now,
                        SenderId = userId,
                        ReceiverId = receiver.Id
                    };

                    _ctx.PrivateMessages.Add(message);

                    await _ctx.SaveChangesAsync();

                    return Page();
                }
            }

            return Page();
        }
    }
}
