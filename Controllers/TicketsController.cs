using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpDeskCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace HelpDeskCore.Controllers
{
    public class TicketsController : Controller
    {
        private readonly HelpDeskCoreContext _context;

        public TicketsController(HelpDeskCoreContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["UserSortParam"] = sortOrder == "User" ? "user_desc" : "User";
            ViewData["AssignedToSortParm"] = sortOrder == "AssignedTo" ? "assignedTo_desc" : "AssignedTo";
            ViewData["CreatedAtSortParam"] = sortOrder == "CreatedAt" ? "createdAt_desc" : "CreatedAt";
            ViewData["CurrentFilter"] = searchString;

            var tickets = from t in _context.TicketsModel
                          where t.IsActive == true
                          select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                tickets = tickets.Where(t => t.Title.Contains(searchString)
                    || t.User.Contains(searchString) || t.AssignedTo.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    tickets = tickets.OrderByDescending(t => t.Title);
                    break;
                case "User":
                    tickets = tickets.OrderBy(t => t.User);
                    break;
                case "user_desc":
                    tickets = tickets.OrderByDescending(t => t.User);
                    break;
                case "AssignedTo":
                    tickets = tickets.OrderBy(t => t.AssignedTo);
                    break;
                case "assignedTo_desc":
                    tickets = tickets.OrderByDescending(t => t.AssignedTo);
                    break;
                case "CreatedAt":
                    tickets = tickets.OrderBy(t => t.CreatedAt);
                    break;
                case "createdAt_desc":
                    tickets = tickets.OrderByDescending(t => t.CreatedAt);
                    break;
                default:
                    tickets = tickets.OrderBy(t => t.Title);
                    break;
            }
            return View(await tickets.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Closed(string sortOrder, string searchString)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["UserSortParam"] = sortOrder == "User" ? "user_desc" : "User";
            ViewData["AssignedToSortParm"] = sortOrder == "AssignedTo" ? "assignedTo_desc" : "AssignedTo";
            ViewData["CreatedAtSortParam"] = sortOrder == "CreatedAt" ? "createdAt_desc" : "CreatedAt";
            ViewData["CurrentFilter"] = searchString;

            var tickets = from t in _context.TicketsModel
                          where t.IsActive == false
                          select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                tickets = tickets.Where(t => t.Title.Contains(searchString)
                    || t.User.Contains(searchString) || t.AssignedTo.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    tickets = tickets.OrderByDescending(t => t.Title);
                    break;
                case "User":
                    tickets = tickets.OrderBy(t => t.User);
                    break;
                case "user_desc":
                    tickets = tickets.OrderByDescending(t => t.User);
                    break;
                case "AssignedTo":
                    tickets = tickets.OrderBy(t => t.AssignedTo);
                    break;
                case "assignedTo_desc":
                    tickets = tickets.OrderByDescending(t => t.AssignedTo);
                    break;
                case "CreatedAt":
                    tickets = tickets.OrderBy(t => t.CreatedAt);
                    break;
                case "createdAt_desc":
                    tickets = tickets.OrderByDescending(t => t.CreatedAt);
                    break;
                default:
                    tickets = tickets.OrderBy(t => t.Title);
                    break;
            }
            return View(await tickets.AsNoTracking().ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketsModel = await _context.TicketsModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketsModel == null)
            {
                return NotFound();
            }

            return View(ticketsModel);
        }


        // GET: Tickets/CloseTicket/5
        public async Task<IActionResult> CloseTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketsModel = await _context.TicketsModel.FindAsync(id);
            if (ticketsModel == null)
            {
                return NotFound();
            }
            return View(ticketsModel);
        }

        // POST: Tickets/CloseTicket/5
        [HttpPost, ActionName("CloseTicket")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseTicketConfirmed(int? id)
        {
            var ticketsModel = await _context.TicketsModel.FindAsync(id);
            ticketsModel.IsActive = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,IsActive,User,AssignedTo,CreatedAt")] TicketsModel ticketsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketsModel);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketsModel = await _context.TicketsModel.FindAsync(id);
            if (ticketsModel == null)
            {
                return NotFound();
            }
            return View(ticketsModel);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,IsActive,User,AssignedTo,CreatedAt")] TicketsModel ticketsModel)
        {
            if (id != ticketsModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketsModelExists(ticketsModel.Id))
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
            return View(ticketsModel);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketsModel = await _context.TicketsModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticketsModel == null)
            {
                return NotFound();
            }

            return View(ticketsModel);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketsModel = await _context.TicketsModel.FindAsync(id);
            _context.TicketsModel.Remove(ticketsModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketsModelExists(int id)
        {
            return _context.TicketsModel.Any(e => e.Id == id);
        }
    }
}
