using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCAssignment.Models
{
    public class LoginInfo
    {
        public int Id { get; set; }

        [DataType(DataType.EmailAddress), Display(Name = "Email Id")]
        public string EmailId { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}