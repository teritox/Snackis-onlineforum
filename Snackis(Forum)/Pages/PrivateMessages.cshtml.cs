using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis_Forum_.Areas.Identity.Data;
using Snackis_Forum_.Data;
using Snackis_Forum_.Models;
using Snackis_Forum_.Services;

namespace Snackis_Forum_.Pages
{
    public class PrivateMessagesModel : PageModel
    {
        private readonly IForumDataService _ds;

        public PrivateMessagesModel(IForumDataService ds)
        {
            _ds = ds;
        }

        public IEnumerable<PrivateMessage> MessageList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            MessageList = await _ds.GetPrivateMessages(user);

            return Page();
        }
    }
}
