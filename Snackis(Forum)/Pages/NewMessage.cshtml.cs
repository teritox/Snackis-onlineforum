using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
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
        public string Title { get; set; } = "";

        [BindProperty]
        public string TextMessage { get; set; } = "";

        [BindProperty]
        public string Receiver { get; set; } = "";


        [BindProperty(SupportsGet = true)]
        public int MessageID { get; set; }

        public bool ReceiverFound { get; set; } = true;

        public bool MessageSent { get; set; } = false;

        public bool MessageFound { get; set; } = true;


        public NewMessageModel(ForumContext ctx, UserManager<ForumUser> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (MessageID != 0)
            {
                var message = await _ctx.PrivateMessages.FindAsync(MessageID);
                Title = "Re: " + message.MessageTitle;

                var sender = _userManager.FindByIdAsync(message.SenderId).Result;
                Receiver = sender.NickName;
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (TextMessage != null)
            {
                if (Receiver != null)
                {
                    if (Title == "" || String.IsNullOrWhiteSpace(Title))
                    {
                        Title = "(Inget ämne)";
                    }

                    var receiver = _userManager.Users.Where(u => u.NickName.ToLower() == Receiver.ToLower()).FirstOrDefault();
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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

                    Title = "";
                    Receiver = "";
                    TextMessage = "";

                    MessageSent = true;

                    return Page();
                }
                else
                {
                    ReceiverFound = false;
                }
            }
            else
            {
                MessageFound = false;
            }

            return Page();
        }
    }
}
