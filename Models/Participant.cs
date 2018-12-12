using System.ComponentModel.DataAnnotations;

namespace Ideas.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int IdeaId { get; set; }
        public Idea Idea { get; set; }
    }
}