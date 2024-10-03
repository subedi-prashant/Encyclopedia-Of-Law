using Encyclopedia_Of_Laws.Data;
using Encyclopedia_Of_Laws.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NToastNotify;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Encyclopedia_Of_Laws.Controllers
{
    public class ClientLetterController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly EncyclopediaOfLawsContext _context;
        private readonly IToastNotification _toastNotification;

        public ClientLetterController(UserManager<ApplicationUser> userManager,
            EncyclopediaOfLawsContext context, IToastNotification toastNotification)
        {
            this.userManager = userManager;
            _context = context;
            _toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult SendLetter()
        {
            return View();
        }

        public async Task<IActionResult> ViewLetter(int id)
        {
            string  requestId = id.ToString();
            
            var letters = await _context.ClientLetters
                                    .Where(l => l.LetterStatus == requestId) 
                                    .ToListAsync();
            
            //if (letters == null || !letters.Any())
            //{
            //    Response.StatusCode = 404;
            //    return View("RequestNotFound", id);
            //}

            return View(letters);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendLetter(ClientLetter clientLetter, int Id)
        {
            //To get the current logged in user ID(Lawyer)
            var lawyerId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var request = _context.Requests.FirstOrDefault(x=>x.RequestId == Id);
            if (ModelState.IsValid) 
            {
                clientLetter.LetterStatus = Id.ToString(); //For Now Using LetterStatus to store RequestId 
                clientLetter.LawyerId = lawyerId;
                clientLetter.UserId = request.UserId;
                clientLetter.LetterDate = DateTime.Now;
               // clientLetter.LetterStatus = "NotReviewed";

                _context.Attach(clientLetter);
                _context.Entry(clientLetter).State = EntityState.Added;
                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Letter sent successfully");

                return RedirectToAction("GetRequestsByLawyer", "Request");

            }
            return View(clientLetter);
        }


       
    }
}
