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

namespace Crypts_And_Coders.Views.CharactersView
{
    [AllowAnonymous]
    public class CharactersViewController : Controller
    {
        private readonly ICharacter _character;

        public CharactersViewController(ICharacter character)
        {
            _character = character;
        }

        // GET: CharactersView
        public async Task<IActionResult> Index()
        {
            var result = await _character.GetCharacters();
            return View(result);
        }

        // GET: CharactersView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CharactersView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CharacterDTO character)
        {
            if (ModelState.IsValid)
            {
                await _character.Create(character);
                return RedirectToAction(nameof(Index));
            }
            return View(character);
        }

        // GET: CharactersView/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _character.GetCharacter((int)id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }

        // POST: CharactersView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _character.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
