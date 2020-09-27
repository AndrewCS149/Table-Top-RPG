using Crypts_And_Coders.Models.DTOs;
using Crypts_And_Coders.Models.Interfaces;
using Crypts_And_Coders.Models.Services;
using System.Threading.Tasks;
using Xunit;

namespace CryptsAndTesters
{
    public class CharacterStatTests : DatabaseTest
    {
        private ICharacterStat BuildRepo()
        {
            return new CharacterStatRepository(_db, _stat);
        }

        [Fact]
        public async Task CanSaveCharacterStat()
        {
            CharacterStatDTO newCharacterStat = new CharacterStatDTO()
            {
                CharacterId = 1,
                StatId = 3,
                Level = 8
            };
            var repo = BuildRepo();

            var saved = await repo.Create(newCharacterStat);

            var result = await repo.GetCharacterStat(1, 3);

            Assert.NotNull(result);
            Assert.Equal(saved.Level, result.Level);
        }

        [Fact]
        public async Task CanGetCharacterStat()
        {
            var repo = BuildRepo();

            var result = await repo.GetCharacterStat(1, 1);

            Assert.NotNull(result);
            Assert.Equal("Charisma", result.Stat.Name);
        }

        [Fact]
        public async Task CanGetAllCharacterStats()
        {
            var repo = BuildRepo();

            var result = await repo.GetCharacterStats(1);

            // Two from seeded data
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task CanUpdateCharacterStat()
        {
            CharacterStatDTO newCharacterStat = new CharacterStatDTO()
            {
                CharacterId = 1,
                StatId = 1,
                Level = 10
            };
            var repo = BuildRepo();

            await repo.Update(newCharacterStat);

            var result = await repo.GetCharacterStat(1, 1);

            Assert.Equal(newCharacterStat.Level, result.Level);
        }

        [Fact]
        public async Task CanDeleteCharacterStat()
        {
            var repo = BuildRepo();

            await repo.Delete(1, 1);

            var count = await repo.GetCharacterStats(1);

            // Took away one of two seeded stats
            Assert.Single(count);
        }
    }
}