using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Snackis_Forum_.Areas.Identity.Data;
using Snackis_Forum_.Models;
using Snackis_Forum_.Services;

namespace Snackis_Forum_.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ForumUser> _userManager;
        private readonly SignInManager<ForumUser> _signInManager;

        public IndexModel(
            UserManager<ForumUser> userManager,
            SignInManager<ForumUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public string CurrentPicture { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ForumUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            CurrentPicture = user.ProfilePicture;

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }




            if (UploadedFile != null)
            {
                if (!System.IO.Directory.Exists("./wwwroot/img")) Directory.CreateDirectory("./wwwroot/img");

                var getUser = await _userManager.FindByIdAsync(user.Id);
                var userPicture = getUser.ProfilePicture;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\", userPicture);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                using var image = Image.Load(UploadedFile.OpenReadStream());
                image.Mutate(x => x.Resize(180, 180));
                image.Save("./wwwroot/img/" + UploadedFile.FileName);

                getUser.ProfilePicture = UploadedFile.FileName;
                await _userManager.UpdateAsync(getUser);
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
