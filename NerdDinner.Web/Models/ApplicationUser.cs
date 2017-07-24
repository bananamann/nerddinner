using System;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.OptionsModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NerdDinner.Web.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        string UserName { get; set; }
    }
}