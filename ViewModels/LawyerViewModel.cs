using Encyclopedia_Of_Laws.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Encyclopedia_Of_Laws.ViewModels
{
    public class LawyerViewModel
    {
        public int LawyerId { get; set; }

        public string Specialization { get; set; }

        public string OfficeNumber { get; set; }

        public string OfficeLocation { get; set; }

        public string Information { get; set; }

        public string JopDescription { get; set; }

        public string UserId { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public List<Request> Reviews { get; set; }

        public List<string> ProfilePics { get; set; }

        public ApplicationUser User {  get; set; } 
    }
}
