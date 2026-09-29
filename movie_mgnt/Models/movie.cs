using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace movie_mgnt.Models
{
    public class movie
    {
        [Display(Name = "movie_id")]
        public int movie_id { get; set; }
        [Required(ErrorMessage = "Enter movie name")]
        public string name { get; set; }
        //[Required(ErrorMessage = "Enter realsed date")]
        public DateTime r_date { get; set; }
        [Required(ErrorMessage = "Enter cat id")]
        public int cat_id { get; set; }
        [Required(ErrorMessage = "Enter rate")]
        public float rate { get; set; }
    }
}