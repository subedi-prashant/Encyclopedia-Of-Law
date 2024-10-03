using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Encyclopedia_Of_Laws.Models;

namespace Encyclopedia_Of_Laws.ViewModels
{
    public class AccountViewModel
    {
    }


    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }

        [Required]
        [Remote(action: "IsUsernameInUse", controller: "Account")]
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

        [MaxLength(100)]
        public string CitizenshipNumber { get; set; }

        [MaxLength(100)]
        public string LicenseNumber { get; set; }

        [MaxLength(100)]
        public string Specialization { get; set; }

        public IFormFile LicensePhoto { get; set; }

        public IFormFile CitizenshipPhoto { get; set; }

        public IFormFile ProfilePicture { get; set; }

        public  LawyerInfo LawyerInfo { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; }
        public List<RoleViewModel> Roles { get; set; }

    }


    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email or Username")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

    }


    public class EditProfileViewModel
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

        public string Gender { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        public IFormFile ProfilePicture { get; set; }
        public string ExistingPhotoPath { get; set; }

        public IList<string> Roles { get; set; }
    }
}
