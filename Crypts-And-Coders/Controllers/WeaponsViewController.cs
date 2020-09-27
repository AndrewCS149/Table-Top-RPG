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

namespace Crypts_And_Coders.Views.WeaponsView
{
    [AllowAnonymous]
    public class WeaponsViewController : Controller
    {
         private readonly IWeapon _weapon;


        public WeaponsViewController(IWeapon weapon)
        {
            _weapon = weapon;
        }

        // GET: WeaponsView
        public async Task<IActionResult> Index()
        {
            return View(await _weapon.GetWeapons());
        }

        // GET: WeaponsView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeaponsView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,BaseDamage")] Weapon weapon)
        {
            if (ModelState.IsValid)
            {
                await _weapon.Create(weapon);
                return RedirectToAction(nameof(Index));
            }
            return View(weapon);
        }

        // GET: WeaponsView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           var weapon = await _weapon.GetWeapon((int)id);
 

            return View(weapon);
        }

        // POST: WeaponsView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _weapon.Delete(id);
           
            return RedirectToAction(nameof(Index));
        }

    }
}
