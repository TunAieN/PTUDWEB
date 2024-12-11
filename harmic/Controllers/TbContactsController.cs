using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using harmic.Models;

namespace harmic.Controllers
{
    public class TbContactsController : Controller
    {
        private readonly Harmic1Context _context;

        public TbContactsController(Harmic1Context context)
        {
            _context = context;
        }
        

        // GET: TbContacts
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: TbContacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbContact = await _context.TbContacts
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (tbContact == null)
            {
                return NotFound();
            }

            return View(tbContact);
        }

        // GET: TbContacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TbContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
		[Route("Contacts/Create")]
		public async Task<IActionResult> Create(string name, string phone, string email, string message)
        {
            try
            {
                TbContact contact = new TbContact();
                contact.Name = name;
                contact.Phone = phone;
                contact.Email = email;
                contact.Message = message;
                contact.CreatedDate = DateTime.Now;
                _context.Add(contact);
				await _context.SaveChangesAsync();
                return Json(new { status = true });
            }
			catch (Exception ex)
			{
				// Log lỗi ra console hoặc lưu log
				Console.WriteLine($"Error: {ex.Message}");
				return Json(new { status = false, message = ex.Message });
			}

		}

		// GET: TbContacts/Edit/5
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbContact = await _context.TbContacts.FindAsync(id);
            if (tbContact == null)
            {
                return NotFound();
            }
            return View(tbContact);
        }

        // POST: TbContacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContactId,Name,Phone,Email,Message,IsRead,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] TbContact tbContact)
        {
            if (id != tbContact.ContactId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbContactExists(tbContact.ContactId))
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
            return View(tbContact);
        }

        // GET: TbContacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbContact = await _context.TbContacts
                .FirstOrDefaultAsync(m => m.ContactId == id);
            if (tbContact == null)
            {
                return NotFound();
            }

            return View(tbContact);
        }

        // POST: TbContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbContact = await _context.TbContacts.FindAsync(id);
            if (tbContact != null)
            {
                _context.TbContacts.Remove(tbContact);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbContactExists(int id)
        {
            return _context.TbContacts.Any(e => e.ContactId == id);
        }
    }
}
