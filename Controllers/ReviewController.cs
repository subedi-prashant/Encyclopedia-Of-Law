using Encyclopedia_Of_Laws.Data;
using Encyclopedia_Of_Laws.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Encyclopedia_Of_Laws.Controllers
{
    public class ReviewController : Controller
    {

        private readonly EncyclopediaOfLawsContext _context;
        private readonly IToastNotification _toastNotification;

        public ReviewController(EncyclopediaOfLawsContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }


        // GET: ReviewController
        public async Task<ActionResult> GetReviewsByUser()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reviews = await _context.Requests.Where(p => p.UserId == userId)
                .Include(p => p.User).Include(p => p.Lawyer).ToListAsync();

            return View(reviews);
        }

        // GET: ReviewController
        public async Task<ActionResult> GetAllLawyerReviews(string id)
        {

            var reviews = await _context.Requests.Where(p => p.LawyerId == id)
                .Include(p => p.Lawyer).Include(p => p.User).ToListAsync();

            return View(reviews);
        }

        // GET: ReviewController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReviewController/Create
        public async Task<IActionResult> AddReview(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            var review = await _context.Requests.Where(p => p.RequestId == id)
                .Include(p => p.Lawyer).Include(p => p.User).FirstOrDefaultAsync();

            if (review == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            return PartialView("_AddOrEditReview", review);
        }

        // POST: ReviewController/AddReview
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReview(Request revieww)
        {
            revieww.ReviewStatus = "Reviewed";
            revieww.ReviewDate = DateTime.Now;

            _context.Attach(revieww);
            _context.Entry(revieww).State = EntityState.Modified;
            _context.SaveChanges();

            _toastNotification.AddSuccessToastMessage("review sent successfully");

            return RedirectToAction("GetRequestsByUser", "Request");
        }



        // GET: ReviewController/Edit/5
        public async Task<IActionResult> EditReview(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            var review = await _context.Requests.Where(p => p.RequestId == id)
                .Include(p => p.Lawyer).Include(p => p.User).FirstOrDefaultAsync();

            if (review == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id);
            }

            return PartialView("_AddOrEditReview", review);
        }

        // POST: ReviewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditReview(Request revieww)
        {

            _context.Attach(revieww);
            _context.Entry(revieww).State = EntityState.Modified;
            _context.SaveChanges();

            _toastNotification.AddSuccessToastMessage("review updated successfully");

            return RedirectToAction("GetRequestsByUser", "Request");
        }

        // GET: ReviewController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReviewController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        
    }
}
