
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Reverse_Text.Data;
using Reverse_Text.Models;
namespace Reverse_Text.Controllers
{
    public class ReverseTextController : Controller
    {
        private readonly Reverse_TextContext _context;
        private readonly ILogger<ReverseTextController> _logger;

        public ReverseTextController(Reverse_TextContext context, ILogger<ReverseTextController> logger)
        {
            _context = context;
            _logger = logger;
        }

    
        // GET: ReverseTextModels
        public async Task<IActionResult> Index()
        {
              return _context.ReverseTextModel != null ? 
                          View(await _context.ReverseTextModel.ToListAsync()) :
                          Problem("Entity set 'Reverse_TextContext.ReverseTextModel'  is null.");
        }

        // GET: ReverseTextModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReverseTextModel == null)
            {
                return NotFound();
            }

            var reverseTextModel = await _context.ReverseTextModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reverseTextModel == null)
            {
                return NotFound();
            }

            return View(reverseTextModel);
        }

        // GET: ReverseTextModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReverseTextModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InputText,OutputText")] ReverseTextModel reverseTextModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reverseTextModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reverseTextModel);
        }

        // GET: ReverseTextModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReverseTextModel == null)
            {
                return NotFound();
            }

            var reverseTextModel = await _context.ReverseTextModel.FindAsync(id);
            if (reverseTextModel == null)
            {
                return NotFound();
            }
            return View(reverseTextModel);
        }

        // POST: ReverseTextModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InputText,OutputText")] ReverseTextModel reverseTextModel)
        {
            if (id != reverseTextModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reverseTextModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReverseTextModelExists(reverseTextModel.Id))
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
            return View(reverseTextModel);
        }

        // GET: ReverseTextModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReverseTextModel == null)
            {
                return NotFound();
            }

            var reverseTextModel = await _context.ReverseTextModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reverseTextModel == null)
            {
                return NotFound();
            }

            return View(reverseTextModel);
        }

        // POST: ReverseTextModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReverseTextModel == null)
            {
                return Problem("Entity set 'Reverse_TextContext.ReverseTextModel'  is null.");
            }
            var reverseTextModel = await _context.ReverseTextModel.FindAsync(id);
            if (reverseTextModel != null)
            {
                _context.ReverseTextModel.Remove(reverseTextModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReverseTextModelExists(int id)
        {
          return (_context.ReverseTextModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [HttpPost]
        public async Task<IActionResult> getReverseText(string inputText)
        {
            try
            {


                _logger.LogInformation("Request for Reverse Text : " + inputText);

               

                using (HttpClient client = new HttpClient())
                {
                    // Replace 'YourApiEndpoint' with your actual API endpoint URL
                    string apiUrl = "https://localhost:7025/api/ReverseText/ReverseText";

                    // Example: Send a POST request with inputText as a JSON payload
                    string requestUrl = $"{apiUrl}?inputText={Uri.EscapeDataString(inputText)}";

                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Handle a successful response
                        var result = await response.Content.ReadAsStringAsync();

                        // Set the content of the label with the API response
                        ViewBag.Response = result;

                        _logger.LogInformation("Receive response for Reverse Text : " + response.StatusCode + " - " + result);

                        var newText = new ReverseTextModel { InputText = inputText, OutputText = result };

                        _context.Add(newText);
                        _context.SaveChanges();

                    }
                    else
                    {
                        ViewBag.Response = $"API Error: {response.StatusCode}";

                        _logger.LogInformation("Receive response for Reverse Text : " + response.StatusCode);


                    }

                    



                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ViewBag.Response = $"An error occurred: {ex.Message}";
            }

            // Return the view with the updated ViewBag
            return View("Index");
        }
    }
}
