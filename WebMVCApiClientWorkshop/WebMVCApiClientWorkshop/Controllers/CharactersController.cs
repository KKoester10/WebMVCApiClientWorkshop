using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DnDCharacter.Models;
using WebMVCApiClientWorkshop.Data;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using WebMVCApiClientWorkshop.Services.Interfaces;

namespace WebMVCApiClientWorkshop.Controllers
{
    public class CharactersController : Controller
    {
        private ICharacterService? _service;
        private IPartyService? _serviceParty;

        private static readonly HttpClient client = new HttpClient();

        private string requestUri = "https://localhost:7190/api/Characters/";

        public CharactersController(ICharacterService service, IPartyService serviceParty)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _serviceParty = serviceParty ?? throw new ArgumentNullException(nameof(serviceParty));

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("User-Agent", "Kolton's API");
        }

        public async Task<IActionResult> Index()
        {
            var response = await _service.FindAll();

            return View(response);
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var character = await _service.FindOne(id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // GET: Characters/Create
        public async Task<IActionResult> Create()
        {
            var response = await _serviceParty.FindAll();
            /*ViewData["AbilitiesId"] = new SelectList(response, "Id", "Id");
            ViewData["InventoryId"] = new SelectList(response, "Id", "Id");*/
            ViewData["PartyId"] = new SelectList(response, "Id", "Id");
            return View();
        }

        // POST: Characters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlayerName,Name,Class,Level,Race,Allignment,Background,ProficiencyBonus,Experiance,ArmorClass,Initiative,HitPoints,Speed,PartyId,AbilitiesId,InventoryId")] Character character)
        {
            character.Id = null;
            var resultPost = await client.PostAsync<Character>(requestUri, character, new JsonMediaTypeFormatter());

            return RedirectToAction(nameof(Index));
        }

        // GET: Characters/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _serviceParty.FindAll();
            /*ViewData["AbilitiesId"] = new SelectList(response, "Id", "Id");
            ViewData["InventoryId"] = new SelectList(response, "Id", "Id");*/
            ViewData["PartyId"] = new SelectList(response, "Id", "Id");
           
            var character = await _service.FindOne(id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: Characters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerName,Name,Class,Level,Race,Allignment,Background,ProficiencyBonus,Experiance,ArmorClass,Initiative,HitPoints,Speed,PartyId,AbilitiesId,InventoryId")] Character character)
        {
            if (id != character.Id)
            {
                return NotFound();
            }

            var resultPut = await client.PutAsync<Character>(requestUri + character.Id.ToString(), character, new JsonMediaTypeFormatter());
            return RedirectToAction(nameof(Index));
        }

        // GET: Characters/Delete/5
        public async Task<IActionResult> Delete(int id)

        {
            var character = await _service.FindOne(id);
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
            var resultDelete = await client.DeleteAsync(requestUri + id.ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}
