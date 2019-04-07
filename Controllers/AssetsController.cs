using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpDeskCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace HelpDeskCore.Controllers
{
    public class AssetsController : Controller
    {
        private readonly HelpDeskCoreContext _context;

        public AssetsController(HelpDeskCoreContext context)
        {
            _context = context;
        }

        // GET: Assets
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["AssetNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "assetName_desc" : "";
            ViewData["SerialSortParam"] = sortOrder == "Serial" ? "serial_desc" : "serial";
            ViewData["ManufacturerToSortParm"] = sortOrder == "Manufacturer" ? "manufacturer_desc" : "Manufacturer";
            ViewData["ModelSortParam"] = sortOrder == "Model" ? "model_desc" : "Model";
            ViewData["CurrentFilter"] = searchString;

            var assets = from a in _context.AssetsModel
                         select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                assets = assets.Where(a => a.AssetName.Contains(searchString)
                    || a.Serial.Contains(searchString)
                    || a.Manufacturer.Contains(searchString)
                    || a.Model.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "assetName_desc":
                    assets = assets.OrderByDescending(a => a.AssetName);
                    break;
                case "Serial":
                    assets = assets.OrderBy(a => a.Serial);
                    break;
                case "serial_desc":
                    assets = assets.OrderByDescending(a => a.Serial);
                    break;
                case "Manufacturer":
                    assets = assets.OrderBy(a => a.Manufacturer);
                    break;
                case "manufacturer_desc":
                    assets = assets.OrderByDescending(a => a.Manufacturer);
                    break;
                case "Model":
                    assets = assets.OrderBy(a => a.Model);
                    break;
                case "model_desc":
                    assets = assets.OrderByDescending(a => a.Model);
                    break;
                default:
                    assets = assets.OrderBy(a => a.AssetName);
                    break;
            }
            return View(await assets.AsNoTracking().ToListAsync());
        }

        // GET: Assets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetsModel = await _context.AssetsModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetsModel == null)
            {
                return NotFound();
            }

            return View(assetsModel);
        }

        // GET: Assets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Assets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AssetName,Serial,Manufacturer,Model")] AssetsModel assetsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assetsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assetsModel);
        }

        // GET: Assets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetsModel = await _context.AssetsModel.FindAsync(id);
            if (assetsModel == null)
            {
                return NotFound();
            }
            return View(assetsModel);
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AssetName,Serial,Manufacturer,Model")] AssetsModel assetsModel)
        {
            if (id != assetsModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assetsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetsModelExists(assetsModel.Id))
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
            return View(assetsModel);
        }

        // GET: Assets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetsModel = await _context.AssetsModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assetsModel == null)
            {
                return NotFound();
            }

            return View(assetsModel);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assetsModel = await _context.AssetsModel.FindAsync(id);
            _context.AssetsModel.Remove(assetsModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetsModelExists(int id)
        {
            return _context.AssetsModel.Any(e => e.Id == id);
        }
    }
}
