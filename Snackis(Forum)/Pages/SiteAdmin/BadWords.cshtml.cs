using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis_Forum_.Models;
using Snackis_Forum_.Services;

namespace Snackis_Forum_.Pages.SiteAdmin
{
    public class BadWordsModel : PageModel
    {
        private readonly IFulaOrdGateway _fulaord;
        public IEnumerable<FulaOrd> FulaOrdList { get; set; }

        public BadWordsModel(IFulaOrdGateway fulaord)
        {
            _fulaord = fulaord;
        }

        [BindProperty(SupportsGet = true)]
        public int DeleteID { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            FulaOrdList = await _fulaord.GetBadWords();

            if (DeleteID != 0)
            {
                await _fulaord.DeleteBadWord(DeleteID);
                return RedirectToPage();
            }

            return Page();
        }

        [BindProperty]
        public string BadWord { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (BadWord != null || BadWord != "")
            {
                FulaOrd ord = new FulaOrd
                {
                    Word = BadWord
                };

                var working = await _fulaord.PostBadWord(ord);

            }

            return RedirectToPage("./BadWords");
        }
    }
}
