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
    public class AbilitiesController : Controller
    {
     
        private IAbilitiesService? _service;

        private static readonly HttpClient client = new HttpClient();

        private string requestUri = "https://localhost:7190/api/Abilities/";

        public AbilitiesController(IAbilitiesService service)
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

        // GET: Abilites/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var abilities = await _service.FindOne(id);
            if (abilities == null)
            {
                return NotFound();
            }

            return View(abilities);
        }

        // GET: Abilities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Abilities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma")] Abilities abilities)
        {
            abilities.Id = null;
            var resultPost = await client.PostAsync<Abilities>(requestUri, abilities, new JsonMediaTypeFormatter());

            return RedirectToAction(nameof(Index));
        }

        // GET: Abilites/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var abilities = await _service.FindOne(id);
            if (abilities == null)
            {
                return NotFound();
            }

            return View(abilities);
        }

        // POST: Abilites/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Strength,Dexterity,Constitution,Intelligence,Wisdom,Charisma")] Abilities abilities)
        {
            if (id != abilities.Id)
            {
                return NotFound();
            }

            var resultPut = await client.PutAsync<Abilities>(requestUri + abilities.Id.ToString(), abilities, new JsonMediaTypeFormatter());
            return RedirectToAction(nameof(Index));
        }

        // GET: Abilites/Delete/5
        public async Task<IActionResult> Delete(int id)

        {
            var abilities = await _service.FindOne(id);
            if (abilities == null)
            {
                return NotFound();
            }

            return View(abilities);
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
