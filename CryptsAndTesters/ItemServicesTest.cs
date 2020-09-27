using Crypts_And_Coders.Models;
using Crypts_And_Coders.Models.DTOs;
using Crypts_And_Coders.Models.Interfaces;
using Crypts_And_Coders.Models.Services;
using System.Threading.Tasks;
using Xunit;

namespace CryptsAndTesters
{
    public class ItemServicesTest : DatabaseTest
    {
        private IItem BuildRepo()
        {
            return new ItemRepository(_db);
        }

        [Fact]
        public async Task CanCreateANewItem()
        {
            // arrange
            Item newItem = new Item()
            {
                Id = 4,
                Name = "Magic Potion",
                Value = "100 cp"
            };

            var repo = BuildRepo();

            ItemDTO newItemDTO = new ItemDTO()
            {
                Id = newItem.Id,
                Name = newItem.Name,
                Value = newItem.Value
            };

            // act
            var createdItem = await repo.Create(newItemDTO);

            // assert
            Assert.NotNull(createdItem);
            Assert.NotEqual(0, createdItem.Id);
            Assert.Equal(4, createdItem.Id);
            Assert.Equal("Magic Potion", createdItem.Name);
        }

        [Fact]
        public async Task CanGetAllItems()
        {
            // arrange
            var repo = BuildRepo();

            // act
            var result = await repo.GetItems();

            // assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task CanGetSingleItem()
        {
            // arrange
            ItemDTO newItem = new ItemDTO()
            {
                Id = 4,
                Name = "Magic Potion",
                Value = "100 cp"
            };

            var repo = BuildRepo();

            ItemDTO newItemDTO = new ItemDTO()
            {
                Id = newItem.Id,
                Name = newItem.Name,
                Value = newItem.Value
            };

            await repo.Create(newItemDTO);

            // act
            var result = await repo.GetItem(4);

            // assert
            Assert.NotNull(result);
            Assert.Equal(4, result.Id);
            Assert.Equal("Magic Potion", result.Name);
        }

        [Fact]
        public async Task CanUpdateAnItem()
        {
            // arrange
            ItemDTO newItem = new ItemDTO()
            {
                Id = 4,
                Name = "Magic Potion",
                Value = "100 cp"
            };

            ItemDTO newItemDTO = new ItemDTO()
            {
                Id = newItem.Id,
                Name = newItem.Name,
                Value = newItem.Value
            };

            var repo = BuildRepo();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            repo.Create(newItemDTO);
            repo.Update(newItemDTO);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            // act
            var result = await repo.GetItem(4);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newItem.Id, result.Id);
            Assert.Equal(newItem.Name, result.Name);
        }

        [Fact]
        public async Task CanDeleteItem()
        {
            // arrange
            var repo = BuildRepo();

            // act
            await repo.Delete(3);
            var result = await repo.GetItems();

            // assert
            Assert.Equal(2, result.Count);
        }
    }
}