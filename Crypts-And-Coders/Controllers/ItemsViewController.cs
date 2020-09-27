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

namespace Crypts_And_Coders.Views.ItemsView
{
    [AllowAnonymous]
    public class ItemsViewController : Controller
    {
        private readonly IItem _item;

        public ItemsViewController(IItem item)
        {
            _item = item;
        }

        // GET: ItemsView
        public async Task<IActionResult> Index()
        {
            var result = await _item.GetItems();
            return View(result);
        }

        // GET: ItemsView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ItemsView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemDTO item)
        {
            if (ModelState.IsValid)
            {
                await _item.Create(item);
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: ItemsView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _item.GetItem((int)id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: ItemsView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _item.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
