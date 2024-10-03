using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Encyclopedia_Of_Laws.Models
{
    public class Request
    {

        [Required]
        public int RequestId { get; set; }

        [Required]
        public string LawyerId { get; set; }

        [Required]
        public string UserId { get; set; }


        public DateTime? RequestDate { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public DateTime? AssignedDate { get; set; }

        public string RequestStatus { get; set; }

        public string ReviewStatus { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public DateTime? ReviewDate { get; set; }

        public virtual ApplicationUser Lawyer { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
