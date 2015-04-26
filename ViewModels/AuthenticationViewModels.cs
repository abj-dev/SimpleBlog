using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBlog.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Username:")]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }


        //[Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-Mail:")]
        [EmailAddress]
        public string Email { get; set; }
    }
}