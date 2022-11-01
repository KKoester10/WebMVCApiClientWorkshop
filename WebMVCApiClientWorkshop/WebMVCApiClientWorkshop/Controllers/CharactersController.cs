﻿using System;
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
    public class CharactersController : Controller
    {
        private readonly WebMVCApiClientWorkshopContext _context;

        public CharactersController(WebMVCApiClientWorkshopContext context)
        {
            _context = context;
        }

        // GET: Characters
        public async Task<IActionResult> Index()
        {
            var webMVCApiClientWorkshopContext = _context.Character.Include(c => c.Abilities).Include(c => c.Inventory).Include(c => c.Party);
            return View(await webMVCApiClientWorkshopContext.ToListAsync());
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Character == null)
            {
                return NotFound();
            }

            var character = await _context.Character
                .Include(c => c.Abilities)
                .Include(c => c.Inventory)
                .Include(c => c.Party)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            ViewData["AbilitiesId"] = new SelectList(_context.Set<Abilities>(), "Id", "Id");
            ViewData["InventoryId"] = new SelectList(_context.Set<CharacterInventory>(), "Id", "Id");
            ViewData["PartyId"] = new SelectList(_context.Party, "Id", "Id");
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerName,Name,Class,Level,Race,Allignment,Background,ProficiencyBonus,Experiance,ArmorClass,Initiative,HitPoints,Speed,PartyId,AbilitiesId,InventoryId")] Character character)
        {
            if (ModelState.IsValid)
            {
                _context.Add(character);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AbilitiesId"] = new SelectList(_context.Set<Abilities>(), "Id", "Id", character.AbilitiesId);
            ViewData["InventoryId"] = new SelectList(_context.Set<CharacterInventory>(), "Id", "Id", character.InventoryId);
            ViewData["PartyId"] = new SelectList(_context.Party, "Id", "Id", character.PartyId);
            return View(character);
        }

        // GET: Characters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Character == null)
            {
                return NotFound();
            }

            var character = await _context.Character.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            ViewData["AbilitiesId"] = new SelectList(_context.Set<Abilities>(), "Id", "Id", character.AbilitiesId);
            ViewData["InventoryId"] = new SelectList(_context.Set<CharacterInventory>(), "Id", "Id", character.InventoryId);
            ViewData["PartyId"] = new SelectList(_context.Party, "Id", "Id", character.PartyId);
            return View(character);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerName,Name,Class,Level,Race,Allignment,Background,ProficiencyBonus,Experiance,ArmorClass,Initiative,HitPoints,Speed,PartyId,AbilitiesId,InventoryId")] Character character)
        {
            if (id != character.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(character);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.Id))
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
            ViewData["AbilitiesId"] = new SelectList(_context.Set<Abilities>(), "Id", "Id", character.AbilitiesId);
            ViewData["InventoryId"] = new SelectList(_context.Set<CharacterInventory>(), "Id", "Id", character.InventoryId);
            ViewData["PartyId"] = new SelectList(_context.Party, "Id", "Id", character.PartyId);
            return View(character);
        }

        // GET: Characters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Character == null)
            {
                return NotFound();
            }

            var character = await _context.Character
                .Include(c => c.Abilities)
                .Include(c => c.Inventory)
                .Include(c => c.Party)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Character == null)
            {
                return Problem("Entity set 'WebMVCApiClientWorkshopContext.Character'  is null.");
            }
            var character = await _context.Character.FindAsync(id);
            if (character != null)
            {
                _context.Character.Remove(character);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterExists(int id)
        {
          return _context.Character.Any(e => e.Id == id);
        }
    }
}
