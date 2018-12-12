using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ideas.Models
{
    public class Idea
    {
        [Key]
        public int IdeaId { get; set; }

        [Display(Name="Post")]
        [Required]
        [MinLength(2, ErrorMessage="Your Idea must be at least 2 characters in length.")]
        public string Post { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public List<Participant> Participants { get; set; }
    }
}