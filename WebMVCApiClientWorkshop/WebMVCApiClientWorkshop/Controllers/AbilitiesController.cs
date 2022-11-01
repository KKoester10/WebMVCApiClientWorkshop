using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DnDCharacter.Models;
using WebMVCApiClientWorkshop.Data;

namespace WebMVCApiClientWorkshop.Controllers
{
    public class AbilitiesController : Controller
    {
        private readonly WebMVCApiClientWorkshopContext _context;

        public AbilitiesController(WebMVCApiClientWorkshopContext context)
        {
            _context = context;
        }

        // GET: Abilities
        public async Task<IActionResult> Index()
        {
              return View(await _context.Abilities.ToListAsync());
        }

        // GET: Abilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Abilities == null)
            {
                return NotFound();
            }

            var abilities = await _context.Abilities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abilities == null)
            {
                return NotFound();
            }

            return View(abilities);
        }

        // GET: Abilities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Abilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma")] Abilities abilities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(abilities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(abilities);
        }

        // GET: Abilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Abilities == null)
            {
                return NotFound();
            }

            var abilities = await _context.Abilities.FindAsync(id);
            if (abilities == null)
            {
                return NotFound();
            }
            return View(abilities);
        }

        // POST: Abilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma")] Abilities abilities)
        {
            if (id != abilities.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abilities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbilitiesExists(abilities.Id))
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
            return View(abilities);
        }

        // GET: Abilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Abilities == null)
            {
                return NotFound();
            }

            var abilities = await _context.Abilities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abilities == null)
            {
                return NotFound();
            }

            return View(abilities);
        }

        // POST: Abilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Abilities == null)
            {
                return Problem("Entity set 'WebMVCApiClientWorkshopContext.Abilities'  is null.");
            }
            var abilities = await _context.Abilities.FindAsync(id);
            if (abilities != null)
            {
                _context.Abilities.Remove(abilities);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbilitiesExists(int id)
        {
          return _context.Abilities.Any(e => e.Id == id);
        }
    }
}
