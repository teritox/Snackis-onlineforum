using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Snackis_Forum_.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ForumUser class
    public class ForumUser : IdentityUser
    {
        [PersonalData]
        public string NickName { get; set; }
    }
}
