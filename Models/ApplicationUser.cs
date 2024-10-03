using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Encyclopedia_Of_Laws.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            RequestLawyers = new HashSet<Request>();
            RequestUsers = new HashSet<Request>();
            ClientLetterLawyers = new HashSet<ClientLetter>();
            ClientLetterUsers = new HashSet<ClientLetter>();
        }


        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string Gender { get; set; }

        public string ProfilePicture { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(100)]
        public string CitizenshipNumber { get; set; }

        [MaxLength(100)]
        public string LicenseNumber { get; set; }

        [MaxLength(100)]
        public string Specialization {  get; set; }
        public virtual LawyerInfo LawyerInfo { get; set; }

        public string LicensePhoto {  get; set; }

        public string CitizenshipPhoto {  get; set; }
        public virtual ICollection<Request> RequestLawyers { get; set; }
        public virtual ICollection<Request> RequestUsers { get; set; }

        public virtual ICollection<ClientLetter> ClientLetterLawyers { get; set; }
        public virtual ICollection<ClientLetter> ClientLetterUsers { get; set; }


    }
}
