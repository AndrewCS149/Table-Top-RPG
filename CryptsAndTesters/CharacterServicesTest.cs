﻿using Crypts_And_Coders.Models;
using Crypts_And_Coders.Models.DTOs;
using Crypts_And_Coders.Models.Interfaces;
using Crypts_And_Coders.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Crypts_And_Coders.Models.SpeciesAndClass;

namespace CryptsAndTesters
{
    public class CharacterServicesTest : DatabaseTest
    {
        private ICharacter BuildRepo()
        {
            return new CharacterRepository(_db, _characterStat, _weapon, _location);
        }

        [Fact]
        public async Task CanSaveCharacter()
        {
            CharacterDTO newChar = new CharacterDTO()
            {
                Name = "Redhawk",
                Species = "Dragonborn",
                Class = "Monk",
                WeaponId = 1,
                LocationId = 1
            };
            var repo = BuildRepo();

            var saved = await repo.Create(newChar);

            Assert.NotNull(saved);
            Assert.NotEqual(0, saved.Id);
            Assert.Equal(saved.Id, newChar.Id);
            Assert.Equal(saved.Name, newChar.Name);
        }

        [Fact]
        public async Task CanGetCharacter()
        {
            var repo = BuildRepo();

            var result = await repo.GetCharacter(1);

            Assert.Equal("Galdifor", result.Name);
        }

        [Fact]
        public async Task CanGetAllCharacters()
        {
            var repo = BuildRepo();

            var result = await repo.GetCharacters();

            // Three from seeded data
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task CanUpdateCharacter()
        {
            CharacterDTO newChar = new CharacterDTO()
            {
                Id = 1,
                Name = "Redhawk",
                Species = "Dragonborn",
                Class = "Monk",
                WeaponId = 1,
                LocationId = 1
            };
            var repo = BuildRepo();

            await repo.Update(newChar);

            var result = await repo.GetCharacter(1);

            Assert.Equal(1, result.Id);
            Assert.Equal(newChar.Name, result.Name);

        }

        [Fact]
        public async Task CanDeleteCharacter()
        {
            var repo = BuildRepo();

            await repo.Delete(1);

            var count = await repo.GetCharacters();

            Assert.Equal(2, count.Count);
        }

        [Fact]
        public async Task CanAddItemToInventory()
        {
            var repo = BuildRepo();

            await repo.AddItemToInventory(1, 3);

            CharacterDTO character = await repo.GetCharacter(1);
            InventoryDTO expected = new InventoryDTO()
            {
                CharacterId = 1,
                ItemId = 3,
                Item = new ItemDTO
                {
                    Id = 3,
                    Name = "Dungeon Key",
                    Value = 100
                }
            };
            Assert.Contains(expected, character.Inventory);
        }

        [Fact]
        public async Task CanRemoveItemFromInventory()
        {
            var repo = BuildRepo();

            await repo.RemoveItemFromInventory(1, 2);

            CharacterDTO character = await repo.GetCharacter(1);
            InventoryDTO expected = new InventoryDTO()
            {
                CharacterId = 1,
                ItemId = 2,
                Item = new ItemDTO
                {
                    Id = 2,
                    Name = "Cup",
                    Value = 5
                }
            };
            Assert.DoesNotContain(expected, character.Inventory);
        }
    }
}
