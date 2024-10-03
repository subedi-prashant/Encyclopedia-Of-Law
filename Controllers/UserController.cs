using Encyclopedia_Of_Laws.Models;
using Encyclopedia_Of_Laws.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Encyclopedia_Of_Laws.Controllers
{
    public class UserController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public UserController(UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> ListLawyers()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            var Lawyers = await userManager.GetUsersInRoleAsync("Lawyer");

            foreach (var Lawyer in Lawyers)
            {
                users.Add(new UserViewModel{
                    Id = Lawyer.Id,
                    FirstName = Lawyer.FirstName,
                    LastName = Lawyer.LastName,
                    Email = Lawyer.Email,
                    UserName = Lawyer.UserName,
                    Gender = Lawyer.Gender,
                    DateOfBirth = Lawyer.DateOfBirth,
                    Country = Lawyer.Country,
                    City = Lawyer.City,
                    Address = Lawyer.Address,
                    Roles = userManager.GetRolesAsync(Lawyer).Result
                });

            }
            return View(users);
        }


        public async Task<IActionResult> ViewLawyer(string id)
        {
            var Lawyer = await userManager.FindByIdAsync(id);

            if (Lawyer == null)
            {
                ViewBag.ErrorMessage = $"Lawyer with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new UserViewModel
            {
                Id = Lawyer.Id,             
                FirstName = Lawyer.FirstName,
                LastName = Lawyer.LastName,
                Email = Lawyer.Email,
                UserName = Lawyer.UserName,
                Gender = Lawyer.Gender,
                DateOfBirth = Lawyer.DateOfBirth,
                Country = Lawyer.Country,
                City = Lawyer.City,
                Address = Lawyer.Address,
                Roles = userManager.GetRolesAsync(Lawyer).Result
            };

            return View(model);
        }
    }
}
