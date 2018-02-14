using System.ComponentModel.DataAnnotations;

namespace Ghost.MVC.Models
{
    public class PlayerNameModel
    {
        [Required(ErrorMessage = "Sorry, you can't play if you don't tell me your name")]
        [Display(Name = "Player name")]
        public string Name { get; set; }
    }
}