using System.ComponentModel.DataAnnotations;
using System;

namespace Encyclopedia_Of_Laws.Models
{
    public class ClientLetter
    {
        [Key]
        public int LettertId { get; set; }

        [Required]
        public string LawyerId { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime? LetterDate { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public DateTime? AssignedDate { get; set; }

        public string LetterStatus { get; set; }

        public virtual ApplicationUser Lawyer { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
