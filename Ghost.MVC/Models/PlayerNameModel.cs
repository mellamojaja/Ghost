using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ghost.MVC.Models
{
    public class PlayerNameModel
    {
        [Required(ErrorMessage = "Sorry, you can't play if you don't tell me your name")]
        [Display(Name = "Player name")]

        public string Name { get; set; }
    }
}