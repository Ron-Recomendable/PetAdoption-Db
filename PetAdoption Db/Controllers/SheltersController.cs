﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoption_Db.Areas.Identity.Data;

namespace PetAdoption_Db.Models
{
    public class SheltersController : Controller
    {
        private readonly PetAdoptionInitialDbContext _context;

        public SheltersController(PetAdoptionInitialDbContext context)
        {
            _context = context;
        }

        // GET: Shelters
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber) //The string searchString, string currentFilter, and int? pageNumber is
                                                                                                                             //for implementing search functionality, filtering and pagination
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LocationSortParm"] = sortOrder == "Location" ? "location_desc" : "Location";
            ViewData["CurrentSort"] = sortOrder;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["currentFilter"] = searchString;
            
          

            var shelter = from s in _context.Shelter
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                shelter = shelter.Where(s => s.Name.Contains(searchString)
                        || s.Location.Contains(searchString)); //For searching Shelter Locations
            }
            switch (sortOrder)
            {
                case "name_desc":
                    shelter = shelter.OrderByDescending(s => s.Name);
                    break;
                default:
                    shelter = shelter.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 5; //limit the page size to only 5 data
            return View(await PaginatedList<Shelter>.CreateAsync(shelter.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Shelters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelter
                .FirstOrDefaultAsync(m => m.ShelterId == id);
            if (shelter == null)
            {
                return NotFound();
            }

            return View(shelter);
        }

        // GET: Shelters/Create
        [Authorize] //This is to restrict the CRUD operations to only logged in Users
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shelters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShelterId,Name,Location,Contact")] Shelter shelter)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(shelter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shelter);
        }

        // GET: Shelters/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelter.FindAsync(id);
            if (shelter == null)
            {
                return NotFound();
            }
            return View(shelter);
        }

        // POST: Shelters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShelterId,Name,Location,Contact")] Shelter shelter)
        {
            if (id != shelter.ShelterId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(shelter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShelterExists(shelter.ShelterId))
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
            return View(shelter);
        }

        // GET: Shelters/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelter
                .FirstOrDefaultAsync(m => m.ShelterId == id);
            if (shelter == null)
            {
                return NotFound();
            }

            return View(shelter);
        }

        // POST: Shelters/Delete/5
    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shelter = await _context.Shelter.FindAsync(id);
            if (shelter != null)
            {
                _context.Shelter.Remove(shelter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShelterExists(int id)
        {
            return _context.Shelter.Any(e => e.ShelterId == id);
        }
    }
}
