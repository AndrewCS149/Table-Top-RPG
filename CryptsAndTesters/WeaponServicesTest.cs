using Crypts_And_Coders.Models;
using Crypts_And_Coders.Models.Interfaces;
using Crypts_And_Coders.Models.Services;
using System.Threading.Tasks;
using Xunit;

namespace CryptsAndTesters
{
    public class WeaponServicesTest : DatabaseTest
    {
        private IWeapon BuildRepo()
        {
            return new WeaponRepository(_db);
        }

        [Fact]
        public async Task CanCreateNewWeapon()
        {
            // arrange
            Weapon newWeapon = new Weapon()
            {
                Id = 4,
                Name = "Mace",
                Type = "Close Range",
                BaseDamage = "1d8"
            };

            var repo = BuildRepo();

            // act
            var createdWeapon = await repo.Create(newWeapon);

            // assert
            Assert.NotNull(createdWeapon);
            Assert.NotEqual(0, createdWeapon.Id);
            Assert.Equal(4, createdWeapon.Id);
            Assert.Equal("Close Range", createdWeapon.Type);
        }

        [Fact]
        public async Task CanGetAllWeapons()
        {
            // arrange
            var repo = BuildRepo();

            // act
            var result = await repo.GetWeapons();

            // assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task CanGetSingleWeapon()
        {
            // arrange
            Weapon newWeapon = new Weapon()
            {
                Id = 4,
                Name = "Mace",
                Type = "Close Range",
                BaseDamage = "1d4"
            };

            var repo = BuildRepo();
            await repo.Create(newWeapon);

            // act
            var result = await repo.GetWeapon(4);

            // assert
            Assert.NotNull(result);
            Assert.Equal(4, result.Id);
            Assert.Equal("Mace", result.Name);
        }

        [Fact]
        public async Task CanUpdateWeapon()
        {
            // arrange
            Weapon newWeapon = new Weapon()
            {
                Id = 4,
                Name = "Mace",
                Type = "Close Range",
                BaseDamage = "1d6"
            };

            var repo = BuildRepo();
            await repo.Create(newWeapon);
            await repo.Update(newWeapon);

            // act
            var result = await repo.GetWeapon(4);

            // assert
            Assert.NotNull(result);
            Assert.Equal(newWeapon.Id, result.Id);
            Assert.Equal(newWeapon.BaseDamage, result.BaseDamage);
        }

        [Fact]
        public async Task CanDeleteWeapon()
        {
            // arrange
            var repo = BuildRepo();

            // act
            await repo.Delete(1);
            var result = await repo.GetWeapons();

            // assert
            Assert.Equal(2, result.Count);
        }
    }
}