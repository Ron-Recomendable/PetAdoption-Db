using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoption_Db.Areas.Identity.Data;
using PetAdoption_Db.Models;

namespace PetAdoption_Db.Controllers
{
    public class PetsController : Controller
    {
        private readonly PetAdoptionInitialDbContext _context;

        public PetsController(PetAdoptionInitialDbContext context)
        {
            _context = context;
        }

        // GET: Pets
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {

      

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["BreedSortParm"] = sortOrder == "Breed" ? "breed_desc" : "Breed";
            ViewData["CurrentSort"] = sortOrder;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["PetsFilter"] = searchString;


            var pet = from p in _context.Pet.Include(p => p.Shelter)
                  
                           select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                pet = pet.Where(p => p.Name.Contains(searchString)
                        || p.Breed.Contains(searchString)
                        || p.Age.Contains(searchString)
                        || p.Sex.Contains(searchString)
                        || p.Description.Contains(searchString));
            }
       

            switch (sortOrder)
            {
                case "name_desc":
                    pet = pet.OrderByDescending(p => p.Name);
                    break;
                case "Breed":
                    pet = pet.OrderBy(p => p.Breed);
                    break;
                case "breed_desc":
                    pet = pet.OrderByDescending(p => p.Breed);
                    break;
                default:
                    pet = pet.OrderBy(p => p.Name);
                    break;
            }
            int pageSize = 3;
   
            return View(await PaginatedList<Pet>.CreateAsync(pet.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Pets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pet
                .Include(p => p.Shelter)
                .FirstOrDefaultAsync(m => m.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // GET: Pets/Create
        public IActionResult Create()
        {
            ViewData["ShelterId"] = new SelectList(_context.Shelter, "ShelterId", "Name");
            return View();

        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to biDnd to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetId,Name,Breed,Age,Sex,Description,ShelterId")] Pet pet)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShelterId"] = new SelectList(_context.Shelter, "ShelterId", "Name", pet.Shelter.Name);
            return View(pet);
        }

        // GET: Pets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pet.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            ViewData["ShelterId"] = new SelectList(_context.Shelter, "ShelterId", "ShelterId", pet.ShelterId);
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetId,Name,Breed,Age,Sex,Description,ShelterId")] Pet pet)
        {
            if (id != pet.PetId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.PetId))
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
            ViewData["ShelterId"] = new SelectList(_context.Shelter, "ShelterId", "ShelterId", pet.ShelterId);
            return View(pet);
        }

        // GET: Pets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pet
                .Include(p => p.Shelter)
                .FirstOrDefaultAsync(m => m.PetId == id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _context.Pet.FindAsync(id);
            if (pet != null)
            {
                _context.Pet.Remove(pet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(int id)
        {
            return _context.Pet.Any(e => e.PetId == id);
        }
    }
}
