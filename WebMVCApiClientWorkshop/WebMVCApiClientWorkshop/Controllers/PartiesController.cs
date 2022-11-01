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
    public class PartiesController : Controller
    {
        private IPartyService? _service;

        private static readonly HttpClient client = new HttpClient();

        private string requestUri = "https://localhost:7190/api/Parties/";

        public PartiesController(IPartyService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("User-Agent", "Kolton's API");
        }

        // Example: https://localhost:7256/api/VideoGames
        public async Task<IActionResult> Index()
        {
            var response = await _service.FindAll();

            return View(response);
        }

        // GET: Parties/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var party = await _service.FindOne(id);
            if (party == null)
            {
                return NotFound();
            }

            return View(party);
        }

        // GET: Parties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Party party)
        {
            party.Id = null;
            var resultPost = await client.PostAsync<Party>(requestUri, party, new JsonMediaTypeFormatter());

            return RedirectToAction(nameof(Index));
        }

        // GET: Parties/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var party = await _service.FindOne(id);
            if (party == null)
            {
                return NotFound();
            }

            return View(party);
        }

        // POST: Parties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Party party)
        {
            if (id != party.Id)
            {
                return NotFound();
            }

            var resultPut = await client.PutAsync<Party>(requestUri + party.Id.ToString(), party, new JsonMediaTypeFormatter());
            return RedirectToAction(nameof(Index));
        }

        // GET: Parties/Delete/5
        public async Task<IActionResult> Delete(int id)

        {
            var party = await _service.FindOne(id);
            if (party == null)
            {
                return NotFound();
            }

            return View(party);
        }

        // POST: Parties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultDelete = await client.DeleteAsync(requestUri + id.ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}
