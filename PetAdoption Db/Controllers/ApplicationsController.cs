using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoption_Db.Areas.Identity.Data;
using PetAdoption_Db.Models;

namespace PetAdoption_Db.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly PetAdoptionInitialDbContext _context;

        public ApplicationsController(PetAdoptionInitialDbContext context)
        {
            _context = context;
        }

        // GET: Applications
        /*public async Task<IActionResult> Index()
        {
            var petAdoptionInitialDbContext = _context.Application.Include(a => a.Pet).Include(a => a.User);
            return View(await petAdoptionInitialDbContext.ToListAsync());
        }*/
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["ApplicationDateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "ApplicationDate_desc" : "";
            ViewData["StatusSortParm"] = sortOrder == "Status" ? "status_desc" : "Status";
            ViewData["CurrentFilter"] = searchString;

            var application = from a in _context.Application
                .Include(a => a.Pet)
                .Include(a => a.User)
                              select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                application = application.Where(a => a.Status.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ApplicationDate_desc":
                    application = application.OrderByDescending(a => a.ApplicationDate);
                    break;
                case "Status":
                    application = application.OrderBy(a => a.Status);
                    break;
                case "Status_desc":
                    application = application.OrderByDescending(u => u.Status);
                    break;
                default:
                    application = application.OrderBy(a => a.ApplicationDate);
                    break;
            }
            
            return View(await application.AsNoTracking().ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .Include(a => a.Pet)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.ApplicationID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["PetID"] = new SelectList(_context.Pet, "PetId", "Name");
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "Username");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicationID,PetID,UserID,Status,ApplicationDate")] Application application)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetID"] = new SelectList(_context.Pet, "PetId", "Name", application.PetID);
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "Username", application.UserID);
            return View(application);
        }

        // GET: Applications/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Application.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["PetID"] = new SelectList(_context.Pet, "PetId", "Name", application.PetID);
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "Username", application.UserID);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationID,PetID,UserID,Status,ApplicationDate")] Application application)
        {
            if (id != application.ApplicationID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ApplicationID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PetID"] = new SelectList(_context.Pet, "PetId", "Name", application.Pet.Name);
            ViewData["UserID"] = new SelectList(_context.User, "UserId", "Username", application.User.Username);
            return View(application);
        }

        // GET: Applications/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Application
                .Include(a => a.Pet)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.ApplicationID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Application.FindAsync(id);
            if (application != null)
            {
                _context.Application.Remove(application);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Application.Any(e => e.ApplicationID == id);
        }
    }
}
