using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class subTextsController : Controller
    {
        private readonly MvcMovieContext _context;

        public subTextsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: subTexts
        public async Task<IActionResult> Index()
        {
            return View(await _context.subText.ToListAsync());
        }

        // GET: subTexts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subText = await _context.subText
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subText == null)
            {
                return NotFound();
            }

            return View(subText);
        }

        // GET: subTexts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: subTexts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProductName,ProductDate,Price,TypeID")] subText subText)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subText);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subText);
        }

        // GET: subTexts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subText = await _context.subText.SingleOrDefaultAsync(m => m.ID == id);
            if (subText == null)
            {
                return NotFound();
            }
            return View(subText);
        }

        // POST: subTexts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductName,ProductDate,Price,TypeID")] subText subText)
        {
            if (id != subText.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subText);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!subTextExists(subText.ID))
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
            return View(subText);
        }

        // GET: subTexts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subText = await _context.subText
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subText == null)
            {
                return NotFound();
            }

            return View(subText);
        }

        // POST: subTexts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subText = await _context.subText.SingleOrDefaultAsync(m => m.ID == id);
            _context.subText.Remove(subText);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool subTextExists(int id)
        {
            return _context.subText.Any(e => e.ID == id);
        }
    }
}
