using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace movie_mgnt.Models
{
    public class user
    {
        //[Display(Name = "user_id")]
        public int user_id { get; set; }
        [Required(ErrorMessage = "Enter username")]
        public string user_name { get; set; }
        [Required(ErrorMessage = "Enter email")]
        public string email_id { get; set; }
        [Required(ErrorMessage = "Enter password")]
        public string user_password { get; set; }
        [Required(ErrorMessage = "Enter city")]
        public string city { get; set; }
        [Required(ErrorMessage = "Enter phone no")]
        public string phone_number { get; set; }

    }
}