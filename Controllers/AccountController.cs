using Encyclopedia_Of_Laws.Models;
using Encyclopedia_Of_Laws.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Encyclopedia_Of_Laws.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private LinkGenerator linkGenerator;


        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment,
            LinkGenerator linkGenerator)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.httpContextAccessor = httpContextAccessor;
            this.hostingEnvironment = hostingEnvironment;
            this.linkGenerator = linkGenerator;
        }


        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email}  is already taken.");
            }
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsUsernameInUse(string username)
        {
            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"username {username}  is already taken.");
            }
        }

        [AllowAnonymous]
        // GET: AccountController
        public async Task<IActionResult> Register()
        {
            //var roles = await roleManager.Roles.Select(r => new RoleViewModel { RoleId = r.Id, RoleName = r.Name }).ToListAsync();
            var roles = await roleManager.Roles
                            .Where(r => r.Name != "User") // Exclude roles with the name "User" since User role is hardcoded in html
                            .Select(r => new RoleViewModel { RoleId = r.Id, RoleName = r.Name })
                            .ToListAsync();

            roles.Insert(0, new RoleViewModel { RoleId = "-1", RoleName = "--- Please select a role ---" });

            var viewmodel = new RegisterViewModel
            {
                Roles = roles
            };

            return View(viewmodel);
            //return roles;
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Role == "-1")
                {
                    ModelState.AddModelError("Roles", "Please select a role from the list!");
                    return View(model);
                }

                string fileName = UploadFile(model.ProfilePicture) ?? string.Empty;
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    Country = model.Country,
                    City = model.City,
                    Address = model.Address,
                    ProfilePicture = fileName
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, model.Role);
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        // GET: AccountController
        public async Task<IActionResult> RegisterLawyer()
        {
            var roles = await roleManager.Roles
                            .Where(r => r.Name != "Lawyer") // Exclude roles with the name "Lawyer" since Lawyer role is hardcoded in html
                            .Select(r => new RoleViewModel { RoleId = r.Id, RoleName = r.Name })
                            .ToListAsync();
            roles.Insert(0, new RoleViewModel { RoleId = "-1", RoleName = "--- Please select a role ---" });

            var viewmodel = new RegisterViewModel
            {
                Roles = roles
            };

            return View(viewmodel);
            //return roles;
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterLawyer(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Role == "-1")
                {
                    ModelState.AddModelError("Roles", "Please select a role from the list!");
                    return View(model);
                }

                string fileName = UploadFile(model.ProfilePicture) ?? string.Empty;
                string citizenship = UploadFile(model.CitizenshipPhoto) ?? string.Empty;
                string license = UploadFile(model.LicensePhoto) ?? string.Empty;

                var lawyerInfo = new LawyerInfo
                {
                    Specialization = model.Specialization,
                    Information = model.LawyerInfo.Information,
                    JopDescription = model.LawyerInfo.JopDescription,
                    OfficeLocation = model.Address,
                    Lat = model.LawyerInfo.Lat,
                    Long = model.LawyerInfo.Long,
                };

                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    CitizenshipNumber = model.CitizenshipNumber,
                    LicenseNumber = model.LicenseNumber,
                    Specialization = model.Specialization,
                    Address = model.Address,
                    ProfilePicture = fileName,
                    CitizenshipPhoto = citizenship,
                    LicensePhoto = license,
                    LawyerInfo = lawyerInfo,
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, model.Role);

                    // Get user ID and generate email confirmation token
                    var userId = await userManager.GetUserIdAsync(user);
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    // Construct confirmation email link
                    var protocol = httpContextAccessor.HttpContext.Request.Scheme;
                    var host = httpContextAccessor.HttpContext.Request.Host;
                    var routeValues = new
                    {
                        userId = userId,
                        code = code
                    };
                    var url = linkGenerator.GetPathByAction("ConfirmEmail", "Account", values: routeValues);
                    var completeUrl = $"{protocol}://{host}{url}";

                    // Send confirmation email
                    await SendEmailAsync(user.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(completeUrl)}'>clicking here</a>.");

                    // Handle account confirmation based on configuration
                    //if (userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    return View("~/Views/Account/RegisterConfirmation.cshtml");
                    //}
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return (2, "Registration successful");
                    //}

                    //await signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("RegisterLawyer");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await userManager.ConfirmEmailAsync(user, code);
            //bool Status = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            bool Status = result.Succeeded ? true : false;
            return View("~/Views/Account/ConfirmEmailStatus.cshtml", Status);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //To enable the userto login with Email or username
            var username = new EmailAddressAttribute().IsValid(model.Email) ? userManager.FindByEmailAsync(model.Email).Result.UserName : model.Email;

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Email);

                if(user is null)
                {
                    ModelState.AddModelError(string.Empty, "No user found");
                    return View(model);
                }

                bool isInRoleLawyer = await userManager.IsInRoleAsync(user, "Lawyer");

                bool isEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);

                if (isInRoleLawyer && !isEmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed for Lawyer");
                    return View(model);
                }

                var result = await signInManager.PasswordSignInAsync(
                    username, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }


        public async Task<IActionResult> Profile(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Lawyer with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditProfileViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Country = user.Country,
                City = user.City,
                Address = user.Address,
                ExistingPhotoPath = user.ProfilePicture,
                Roles = userManager.GetRolesAsync(user).Result
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Lawyer with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditProfileViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Country = user.Country,
                City = user.City,
                Address = user.Address,
                ExistingPhotoPath = user.ProfilePicture,
                Roles = userManager.GetRolesAsync(user).Result
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                string fileName = UploadFile(model.ProfilePicture) ?? string.Empty;

                user.Email = model.Email;
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;
                user.ProfilePicture = fileName;
                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Profile", new { id = userId });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }





        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hostingEnvironment.WebRootPath, "images");
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
                string fileExtension = Path.GetExtension(file.FileName);

                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");

                string newFileName = $"{fileNameWithoutExtension}_{timestamp}{fileExtension}";

                string fullPath = Path.Combine(uploads, newFileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));

                return newFileName;
            }

            return null;
        }


        string UploadFile(IFormFile file, string imageUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hostingEnvironment.WebRootPath, "images");

                string newPath = Path.Combine(uploads, file.FileName);
                string oldPath = Path.Combine(uploads, imageUrl);

                if (oldPath != newPath)
                {
                    System.IO.File.Delete(oldPath);
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }

                return file.FileName;
            }

            return imageUrl;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                string fromMail = "*******@gmail.com";
                string fromPassword = "*********";

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true
                };
                var message = new MailMessage
                {
                    From = new MailAddress(fromMail),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };

                message.To.Add(email);

                //Send the Mail
                await smtpClient.SendMailAsync(message);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
