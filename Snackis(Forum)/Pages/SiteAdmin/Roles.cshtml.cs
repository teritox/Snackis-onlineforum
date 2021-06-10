using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Snackis_Forum_.Areas.Identity.Data;

namespace Snackis_Forum_.Pages.SiteAdmin
{
    public class RolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserManager<ForumUser> _userManager;


        public List<IdentityRole> Roles { get; set; }
        public List<string> UserRoles { get; set; }


        public IQueryable<ForumUser> Users { get; set; }
        public ForumUser CurrentUser { get; set; }



        [BindProperty(SupportsGet = true)]
        public string AddUserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RemoveUserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Role { get; set; }

        [BindProperty]
        public string RoleName { get; set; }


        public bool isUser { get; set; }
        public bool isAdmin { get; set; }


        public RolesModel(RoleManager<IdentityRole> roleManager, UserManager<ForumUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            Roles = _roleManager.Roles.ToList();
            Users = _userManager.Users;

            if (AddUserId != null)
            {
                var userToAdd = await _userManager.FindByIdAsync(AddUserId);
                var roleresult = await _userManager.AddToRoleAsync(userToAdd, Role);
            }

            if (RemoveUserId != null)
            {
                var userToDelete = await _userManager.FindByIdAsync(RemoveUserId);
                var roleresult = await _userManager.RemoveFromRoleAsync(userToDelete, Role);
            }


            CurrentUser = await _userManager.GetUserAsync(User);

            isUser = await _userManager.IsInRoleAsync(CurrentUser, "User");
            isAdmin = await _userManager.IsInRoleAsync(CurrentUser, "Admin");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Users = _userManager.Users;
            if (RoleName != null)
            {
                await CreateRole(RoleName);
            }
            return RedirectToPage("./index");
        }

        public async Task CreateRole(string roleName)
        {
            bool exist = await _roleManager.RoleExistsAsync(roleName);
            if (!exist)
            {
                // first we create Admin role 
                var role = new IdentityRole
                {
                    Name = roleName
                };
                await _roleManager.CreateAsync(role);
            }
        }
    }
}
