using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Encyclopedia_Of_Laws.Data;
using Encyclopedia_Of_Laws.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Encyclopedia_Of_Laws.ViewModels;

namespace Encyclopedia_Of_Laws.Controllers
{
    public class LawyerInfoController : Controller
    {
        private readonly EncyclopediaOfLawsContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public LawyerInfoController(EncyclopediaOfLawsContext context, UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
    
        // GET: LawyerInfo
        public IActionResult Index(string SearchText = "")
        {
            List<LawyerInfo> lawyers;

            if (SearchText != "" && SearchText != null)
            {
                lawyers = _context.LawyerInfos.Include(l => l.User)
                   .Where(l => l.User.FirstName.Contains(SearchText)
                   || l.User.LastName.Contains(SearchText)
                   || l.User.UserName.Contains(SearchText)
                   || l.User.Email.Contains(SearchText)
                   || l.User.Address.Contains(SearchText)
                   || l.User.City.Contains(SearchText)
                   || l.User.Country.Contains(SearchText)
                   || l.Information.Contains(SearchText)
                   || l.Specialization.Contains(SearchText)
                   || l.JopDescription.Contains(SearchText))
                   .ToList();
            }

            else    
                lawyers = _context.LawyerInfos.Include(l => l.User).Include(l => l.User.RequestLawyers).ToList();
            return View(lawyers);
        }


        // GET: LawyerInfo/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null){
                return NotFound();
            }

            var lawyerInfo = await _context.LawyerInfos.Include(l => l.User).Include(l => l.User.RequestUsers)
                .FirstOrDefaultAsync(l => l.UserId == id);

            var reviews = _context.Requests.Include(r => r.User).Where(r => r.ReviewStatus == "Reviewed").Where(l => l.LawyerId == id).ToList();

            var profilePics = reviews.Select(r => r.User.ProfilePicture).ToList(); 

            var model = new LawyerViewModel
            {
                LawyerId = lawyerInfo.LawyerId,
                UserId = lawyerInfo.UserId,
                Email = lawyerInfo.User.Email,
                UserName = lawyerInfo.User.UserName,
                FirstName = lawyerInfo.User.FirstName,
                LastName = lawyerInfo.User.LastName,
                Country = lawyerInfo.User.Country,
                City = lawyerInfo.User.City,
                Address = lawyerInfo.User.Address,
                Specialization = lawyerInfo.Specialization,
                Information = lawyerInfo.Information,
                JopDescription = lawyerInfo.JopDescription,
                OfficeLocation = lawyerInfo.OfficeLocation,
                OfficeNumber = lawyerInfo.OfficeNumber,
                Reviews = reviews,
                User = lawyerInfo.User,
                ProfilePics = profilePics,
            };
            return View(model);
        }


        // GET: LawyerInfo/Profile/5
        public async Task<IActionResult> Profile(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
       
            var lawyerInfo = await _context.LawyerInfos.Include(l => l.User).FirstOrDefaultAsync(m => m.UserId == id);
            
            if (lawyerInfo == null)
            {
                return View();
            }

            return View(lawyerInfo);
        }



        // GET: LawyerInfo/Create
        public IActionResult Create()
        {
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: LawyerInfo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LawyerId,Specialization,OfficeNumber,OfficeLocation,Information,JopDescription,UserId")] LawyerInfo lawyerInfo)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                lawyerInfo.UserId = userId;
                _context.Add(lawyerInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Profile", new { id = userId });
            }
            return View(lawyerInfo);
        }

        // GET: LawyerInfo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lawyerInfo = await _context.LawyerInfos.FindAsync(id);
            if (lawyerInfo == null)
            {
                return NotFound();
            }
            return View(lawyerInfo);
        }

        // POST: LawyerInfo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LawyerInfo lawyerInfo)
        {
            if (id != lawyerInfo.LawyerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                try
                {
                    lawyerInfo.UserId = userId;
                    _context.Update(lawyerInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LawyerInfoExists(lawyerInfo.LawyerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Profile", new { id = userId });
            }
            return View(lawyerInfo);
        }

        //// GET: LawyerInfo/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var lawyerInfo = await _context.LawyerInfos
        //        .Include(l => l.User)
        //        .FirstOrDefaultAsync(m => m.LawyerId == id);
        //    if (lawyerInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(lawyerInfo);
        //}

        //// POST: LawyerInfo/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var lawyerInfo = await _context.LawyerInfos.FindAsync(id);
        //    _context.LawyerInfos.Remove(lawyerInfo);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        public List<Request> GetNotifications()
        {
            var LawyerId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.LawyerId = LawyerId;
            var getNotification = _context.Requests.Where(c => c.LawyerId == LawyerId && c.RequestStatus == "Pending").ToList();
            return getNotification;
        }


        private bool LawyerInfoExists(int id)
        {
            return _context.LawyerInfos.Any(e => e.LawyerId == id);
        }

    }
}
