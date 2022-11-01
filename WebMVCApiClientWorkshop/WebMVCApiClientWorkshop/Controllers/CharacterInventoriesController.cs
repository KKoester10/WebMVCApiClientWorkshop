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
    public class CharacterInventoriesController : Controller
    {
        private readonly WebMVCApiClientWorkshopContext _context;

        public CharacterInventoriesController(WebMVCApiClientWorkshopContext context)
        {
            _context = context;
        }

        // GET: CharacterInventories
        public async Task<IActionResult> Index()
        {
              return View(await _context.CharacterInventory.ToListAsync());
        }

        // GET: CharacterInventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CharacterInventory == null)
            {
                return NotFound();
            }

            var characterInventory = await _context.CharacterInventory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characterInventory == null)
            {
                return NotFound();
            }

            return View(characterInventory);
        }

        // GET: CharacterInventories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CharacterInventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemName,Amount")] CharacterInventory characterInventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(characterInventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(characterInventory);
        }

        // GET: CharacterInventories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CharacterInventory == null)
            {
                return NotFound();
            }

            var characterInventory = await _context.CharacterInventory.FindAsync(id);
            if (characterInventory == null)
            {
                return NotFound();
            }
            return View(characterInventory);
        }

        // POST: CharacterInventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemName,Amount")] CharacterInventory characterInventory)
        {
            if (id != characterInventory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(characterInventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterInventoryExists(characterInventory.Id))
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
            return View(characterInventory);
        }

        // GET: CharacterInventories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CharacterInventory == null)
            {
                return NotFound();
            }

            var characterInventory = await _context.CharacterInventory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characterInventory == null)
            {
                return NotFound();
            }

            return View(characterInventory);
        }

        // POST: CharacterInventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CharacterInventory == null)
            {
                return Problem("Entity set 'WebMVCApiClientWorkshopContext.CharacterInventory'  is null.");
            }
            var characterInventory = await _context.CharacterInventory.FindAsync(id);
            if (characterInventory != null)
            {
                _context.CharacterInventory.Remove(characterInventory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterInventoryExists(int id)
        {
          return _context.CharacterInventory.Any(e => e.Id == id);
        }
    }
}
