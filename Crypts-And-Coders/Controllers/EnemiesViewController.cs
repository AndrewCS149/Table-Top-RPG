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

namespace Crypts_And_Coders.Views.EnemiesView
{
    [AllowAnonymous]

    public class EnemiesViewController : Controller
    {
        private readonly IEnemy _enemy;

        public EnemiesViewController(IEnemy enemy)
        {
            _enemy = enemy;
        }

        // GET: EnemiesView
        public async Task<IActionResult> Index()
        {
            var result = await _enemy.GetEnemies();
            return View(result);
        }

        // GET: EnemiesView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EnemiesView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnemyDTO enemy)
        {
            if (ModelState.IsValid)
            {
                await _enemy.Create(enemy);
                return RedirectToAction(nameof(Index));
            }
            return View(enemy);
        }

        // GET: EnemiesView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enemy = await _enemy.GetEnemy((int)id);
            if (enemy == null)
            {
                return NotFound();
            }

            return View(enemy);
        }

        // POST: EnemiesView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _enemy.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
