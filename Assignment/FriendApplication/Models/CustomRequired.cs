using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FriendApplication.Models
{
    /// <summary>
    /// Custom Required attribute for displaying the custom message instead of default.
    /// </summary>
    public class CustomRequired : RequiredAttribute
    {
        public CustomRequired()
        {
            this.ErrorMessage = "Friend Name Empty Not Allowed.(Also Under 30 Characters)";
        }
    }
}