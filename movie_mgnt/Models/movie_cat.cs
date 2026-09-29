using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace movie_mgnt.Models
{
    public class movie_cat
    {
        [Display(Name = "cat_id")]
        public int cat_id { get; set; }

        [Required(ErrorMessage = "Enter type")]
        public string type { get; set; }
    }
}