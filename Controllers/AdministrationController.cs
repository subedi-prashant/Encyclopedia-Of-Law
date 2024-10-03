using Encyclopedia_Of_Laws.Data;
using Encyclopedia_Of_Laws.Models;
using Encyclopedia_Of_Laws.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Encyclopedia_Of_Laws.Controllers
{

    public class AdministrationController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly EncyclopediaOfLawsContext _context;
        private readonly IToastNotification _toastNotification;


        public AdministrationController(UserManager<ApplicationUser> userManager,
            EncyclopediaOfLawsContext context, IToastNotification toastNotification,
           RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            _context = context;
            _toastNotification = toastNotification;
            this.roleManager = roleManager;
        }
    


        //MANAGE ROLES
        [HttpGet]
        public async Task<IActionResult> ListRoles()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return View(roles);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(AddRoleViewModel model)
        {
            if (!ModelState.IsValid)
                return View("ListRoles", await roleManager.Roles.ToListAsync());

            if (await roleManager.RoleExistsAsync(model.RoleName))
            {
                ModelState.AddModelError("Name", "Role is already exists!");
                return View("ListRoles", await roleManager.Roles.ToListAsync());
            }

            await roleManager.CreateAsync(new IdentityRole(model.RoleName.Trim()));

            return RedirectToAction(nameof(ListRoles));

        }



        //MANAGE USERS

        public async Task<IActionResult> ListUsers()
        {
            var users = await userManager.Users.Select(user => new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth,
                Country = user.Country,
                City = user.City,
                Address = user.Address,
                Roles = userManager.GetRolesAsync(user).Result
            }).ToListAsync();

            return View(users);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                if (id == null)
                {
                    Response.StatusCode = 404;
                    return View("RequestNotFound", id);
                }

                var user = await userManager.FindByIdAsync(id);
                if (user == null)
                {
                    Response.StatusCode = 404;
                    return View("RequestNotFound", id);
                }
                var roles = await userManager.GetRolesAsync(user);

                if (roles.Contains("Lawyer"))
                {
                    var relatedRequests = _context.Requests.Where(r => r.LawyerId == id);
                    _context.Requests.RemoveRange(relatedRequests);

                    var relatedLetters = _context.ClientLetters.Where(x => x.LawyerId == id);
                    _context.ClientLetters.RemoveRange(relatedLetters);
                }

                if(roles.Contains("User"))
                {
                    var relatedRequests = _context.Requests.Where(r => r.UserId == id);
                    _context.Requests.RemoveRange(relatedRequests);

                    var relatedLetters = _context.ClientLetters.Where(x => x.UserId == id);
                    _context.ClientLetters.RemoveRange(relatedLetters);
                }
                
                var lawyerInfo = _context.LawyerInfos.FirstOrDefault(l => l.UserId == id);
                if (lawyerInfo != null)
                {
                    _context.LawyerInfos.Remove(lawyerInfo); 
                }

                await userManager.DeleteAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<IActionResult> VerifyLawyer(string id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            user.PhoneNumberConfirmed = true;
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return View("RequestNotFound",id);
            }
        }


        //USERS ISSUES

        public async Task<IActionResult> ListUserIssues()
        {
            var UserIssues = await _context.UserIssues.ToListAsync();
            return View(UserIssues);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendIssue(UserIssue issues)
        {
            if (!ModelState.IsValid)
                return PartialView("_Contact", issues);

            issues.IssueDate = DateTime.Now;
            _context.Attach(issues);
            _context.Entry(issues).State = EntityState.Added;
            _context.SaveChanges();

            _toastNotification.AddSuccessToastMessage("Message sent successfully");
            return RedirectToAction("Index", "Home");

        }


    }
}
