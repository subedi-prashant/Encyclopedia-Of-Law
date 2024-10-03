using Encyclopedia_Of_Laws.Data;
using Encyclopedia_Of_Laws.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using NToastNotify;

namespace Encyclopedia_Of_Laws.Controllers
{
    public class RequestController : Controller
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly EncyclopediaOfLawsContext _context;
        private readonly IToastNotification _toastNotification;

        public RequestController(UserManager<ApplicationUser> userManager,
            EncyclopediaOfLawsContext context, IToastNotification toastNotification)
        {
            this.userManager = userManager;
            _context = context;
            _toastNotification = toastNotification;
        }

        //USER ACTIONS

        // GET: RequestController/GetRequestsByUser/5
        public async Task<IActionResult> GetRequestsByUser()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var requests = await _context.Requests.Where(p => p.UserId == userId).OrderByDescending(p => p.RequestDate)
                .Include(p => p.Lawyer).Include(p => p.User).ToListAsync();
            return View(requests);
        }

        // GET: RequestController/GetScheduleByUser/5
        public async Task<IActionResult> GetScheduleByUser()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var schedule = await _context.Requests.Where(p => p.UserId == userId)
                .OrderByDescending(p => p.AssignedDate)
                .Include(p => p.Lawyer).Include(p => p.User).ToListAsync();

            return View(schedule);
        }


        // GET: RequestController/GetDetailsByUser/5
        public async Task<IActionResult> GetDetailsByUser(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            var request = await _context.Requests.Where(p => p.RequestId == id)
                .Include(p => p.Lawyer).Include(p => p.User).FirstOrDefaultAsync();
            if (request == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            return PartialView("GetDetailsByUser", request);
        }

        // GET: RequestController/SendRequest
        public ActionResult SendRequest()
        {
            return View();
        }

        // POST: RequestController/SendRequest
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendRequest(Request request, string Id)
        {
            //To get the current logged in user ID
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                request.LawyerId = Id;
                request.UserId = userId;
                request.RequestDate = DateTime.Now;
                request.ReviewStatus = "NotReviewed";


                _context.Attach(request);
                _context.Entry(request).State = EntityState.Added;
                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Request sent successfully");
                return RedirectToAction(nameof(GetRequestsByUser));
            }
            return View(request);
        }

        // GET: RequestController/EditByUser/5
        public async Task<IActionResult> EditByUser(int? id)
        {

            if (id == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            var request = await _context.Requests.Where(p => p.RequestId == id)
                .Include(p => p.Lawyer).Include(p => p.User).FirstOrDefaultAsync();
            if (request == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            return PartialView("_AddOrEditRequest", request);
        }

        // POST: RequestController/EditByUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditByUser(Request request)
        {
            _context.Attach(request);
            _context.Entry(request).State = EntityState.Modified;
            _context.SaveChanges();

            _toastNotification.AddSuccessToastMessage("Request sent successfully");
            return RedirectToAction(nameof(GetRequestsByUser));
        }

        // GET: RequestController/CancelByUser/5
        public async Task<IActionResult> DeleteByUser(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            _context.Requests.Remove(request);
            _context.SaveChanges();

            return Ok();
        }



        // GET: RequestController/AcceptDate/5
        public async Task<IActionResult> ConfirmDate(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            request.RequestStatus = "Confirmed";

            _context.Attach(request);
            _context.Entry(request).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }

        // GET: RequestController/CancelDate/5
        public async Task<IActionResult> CancelDate(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            request.RequestStatus = "Cancelled";

            _context.Attach(request);
            _context.Entry(request).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }



        //LAWYERS ACTIONS

        // GET: RequestController/GetRequestsByLawyer/5
        public async Task<IActionResult> GetRequestsByLawyer()
        {
            var LawyerId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var requests = await _context.Requests.Where(p => p.LawyerId == LawyerId)
                .OrderByDescending(p => p.RequestDate)
                .Include(p => p.Lawyer).Include(p => p.User).ToListAsync();
            return View("GetRequestsByUser", requests);

        }


        // GET: RequestController/GetScheduleByLawyer/5
        public async Task<IActionResult> GetScheduleByLawyer()
        {
            var LawyerId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var schedule = await _context.Requests.Where(p => p.LawyerId == LawyerId)
                .OrderByDescending(p => p.AssignedDate)
                .Include(p => p.Lawyer).Include(p => p.User).ToListAsync();

            return View("GetScheduleByUser", schedule);
        }




        // GET: RequestController/AssignDate/5
        public async Task<IActionResult> AssignDate(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            var request = await _context.Requests.Where(p => p.RequestId == id)
                .Include(p => p.Lawyer).Include(p => p.User).FirstOrDefaultAsync();
            if (request == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            return PartialView("_AssignDate", request);
        }

        // POST: RequestController/AssignDate/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignDate(Request request)
        {
            request.RequestStatus = "Assigned";
            _context.Attach(request);
            _context.Entry(request).State = EntityState.Modified;
            _context.SaveChanges();

            _toastNotification.AddSuccessToastMessage("Date assigned successfully");
            return RedirectToAction(nameof(GetRequestsByLawyer));
        }

        // GET: RequestController/RejectRequest/5
        public async Task<IActionResult> RejectRequest(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            request.RequestStatus = "Rejected";

            _context.Attach(request);
            _context.Entry(request).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }


        // GET: RequestController/CompleteRequest/5
        public async Task<IActionResult> CompleteRequest(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            request.RequestStatus = "Completed";

            _context.Attach(request);
            _context.Entry(request).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }


    }
}
