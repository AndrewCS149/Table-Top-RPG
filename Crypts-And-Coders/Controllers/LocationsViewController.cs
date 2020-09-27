using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Crypts_And_Coders.Data;
using Crypts_And_Coders.Models;
using Microsoft.AspNetCore.Authorization;
using Crypts_And_Coders.Models.Interfaces;
using Crypts_And_Coders.Models.DTOs;

namespace Crypts_And_Coders.Views.LocationsView
{
    [AllowAnonymous]
    public class LocationsViewController : Controller
    {
        private readonly ILocation _location;

        public LocationsViewController(ILocation location)
        {
            _location = location;
        }

        // GET: LocationsView
        public async Task<IActionResult> Index()
        {
            var result = await _location.GetLocations();
            return View(result);
        }

        // GET: LocationsView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocationsView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] LocationDTO location)
        {
            if (ModelState.IsValid)
            {
                await _location.Create(location);
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        // GET: LocationsView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _location.GetLocation((int)id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: LocationsView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _location.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
