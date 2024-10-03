using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Encyclopedia_Of_Laws.Models
{
    public class LawyerInfo
    {
        [Key]
        public int LawyerId { get; set; }
        public string Specialization { get; set; }
        public string OfficeNumber { get; set; }
        public string OfficeLocation { get; set; }
        public string Information { get; set; }
        public string JopDescription { get; set; }
        public string Lat {  get; set; }
        public string Long { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; } 
        public string Instagram { get; set; }   
        public string LinkedIn { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
