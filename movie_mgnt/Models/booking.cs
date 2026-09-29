using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace movie_mgnt.Models
{
    public class booking
    {
        [Display(Name ="booking_id")]
        public int booking_id { get; set; }
        [Required(ErrorMessage = "Enter user_id")]
        public int user_id { get; set; }
        [Required(ErrorMessage = "Enter cat_id")]

        public int cat_id{ get; set; }
        [Required(ErrorMessage = "Enter cat_name")]
        public string cat_name { get; set; }
        [Required(ErrorMessage = "Enter movie_id")]
        public int movie_id {  get; set; }
        [Required(ErrorMessage = "Enter movie_name")]
        public string movie_name { get; set; }
        [Required(ErrorMessage = "Enter no of tickets")]
        public int no_tickets {  get; set; }
        [Required (ErrorMessage ="Enter amolunt")]
        public int amount {  get; set; }
    }
}