using Crypts_And_Coders.Models.DTOs;
using Crypts_And_Coders.Models.Interfaces;
using Crypts_And_Coders.Models.Services;
using System.Threading.Tasks;
using Xunit;

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
        public void CanGetCharacterSync()
        {
            var repo = BuildRepo();

            string result = repo.GetCharacterUserNameSync(1);

            Assert.Equal("Seed", result);
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
        public async Task CanReturnFalseOnUnavailableDelete()
        {
            var repo = BuildRepo();

            bool result = await repo.Delete(4);

            Assert.False(result);
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
                    Value = "100 cp"
                }
            };
            bool found = false;
            foreach (var item in character.Inventory)
            {
                if (item.ItemId == expected.ItemId)
                {
                    Assert.Equal(expected.CharacterId, item.CharacterId);
                    Assert.Equal(expected.Item.Name, item.Item.Name);
                    found = true;
                }
            }
            Assert.True(found);
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
                    Value = "100 cp"
                }
            };
            Assert.DoesNotContain(expected, character.Inventory);
        }

        [Fact]
        public async Task CanGetPlayerItemNumber()
        {
            var repo = BuildRepo();

            var result = await repo.GetPlayerItems(1);

            // Should have 2 items from seed
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}