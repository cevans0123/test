using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ideas.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name="Name")]
        [Required (ErrorMessage="What's your name again?")]
        [MinLength(2, ErrorMessage="Your name must be at least 2 characters in length.")]
        public string Name { get; set; }

        [Display(Name="Alias")]
        [Required (ErrorMessage="We need your last name too.")]
        [MinLength(2, ErrorMessage="Your alias must be at least 2 characters in length.")]
        public string Alias { get; set; }

        [Display(Name="Email")]
        [Required (ErrorMessage="Email please! Otherwise we won't know where to spam you.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name="Password")]
        [Required (ErrorMessage="I know your password but put it again anyways.")]
        [MinLength(8, ErrorMessage="Your password must be at least 8 characters in length.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="Confirm Password")]
        [Required]
        [NotMapped]
        [Compare("Password", ErrorMessage = "Password and its confirmation must match.")]
        [DataType(DataType.Password)]
        public string ConfirmPW { get; set; }

        public List<Idea> Ideas { get; set; }
        public List<Participant> Participants { get; set; }
    }
}