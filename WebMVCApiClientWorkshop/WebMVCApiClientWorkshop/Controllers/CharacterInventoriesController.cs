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
    public class CharacterInventoriesController : Controller
    {
        private ICharacterInventoryService? _service;

        private static readonly HttpClient client = new HttpClient();

        private string requestUri = "https://localhost:7190/api/CharacterInventories/";

        public CharacterInventoriesController(ICharacterInventoryService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));

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

        // GET: CharacterInventories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var characterInventory = await _service.FindOne(id);
            if (characterInventory == null)
            {
                return NotFound();
            }

            return View(characterInventory);
        }

        // GET: CharacterInventories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CharacterInventories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemName,Amount")] CharacterInventory characterInventory)
        {
            characterInventory.Id = null;
            var resultPost = await client.PostAsync<CharacterInventory>(requestUri, characterInventory, new JsonMediaTypeFormatter());

            return RedirectToAction(nameof(Index));
        }

        // GET: CharacterInventories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var characterInventory = await _service.FindOne(id);
            if (characterInventory == null)
            {
                return NotFound();
            }

            return View(characterInventory);
        }

        // POST: CharacterInventories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemName,Amount")] CharacterInventory characterInventory)
        {
            if (id != characterInventory.Id)
            {
                return NotFound();
            }

            var resultPut = await client.PutAsync<CharacterInventory>(requestUri + characterInventory.Id.ToString(), characterInventory, new JsonMediaTypeFormatter());
            return RedirectToAction(nameof(Index));
        }

        // GET: CharacterInventories/Delete/5
        public async Task<IActionResult> Delete(int id)

        {
            var characterInventory = await _service.FindOne(id);
            if (characterInventory == null)
            {
                return NotFound();
            }

            return View(characterInventory);
        }

        // POST: VideoGame/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultDelete = await client.DeleteAsync(requestUri + id.ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}
