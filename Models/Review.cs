using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Encyclopedia_Of_Laws.Models
{
    public class Review
    {
        [Required]
        public int ReviewId { get; set; }

        public int Request_Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime? ReviewDate { get; set; }

        public virtual Request Request { get; set; }
    }
}
