using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Encyclopedia_Of_Laws.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
      
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required] 
        public string UserName { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Required, MaxLength(50)]
        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
