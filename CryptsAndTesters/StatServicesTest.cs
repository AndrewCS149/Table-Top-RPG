using Crypts_And_Coders.Models.DTOs;
using Crypts_And_Coders.Models.Interfaces;
using Crypts_And_Coders.Models.Services;
using System.Threading.Tasks;
using Xunit;

namespace CryptsAndTesters
{
    public class StatServicesTest : DatabaseTest
    {
        private IStat BuildRepo()
        {
            return new StatRepository(_db);
        }

        [Fact]
        public async Task CanSaveStat()
        {
            StatDTO newStat = new StatDTO()
            {
                Id = 4,
                Name = "Agility",
            };
            var repo = BuildRepo();

            var saved = await repo.Create(newStat);

            Assert.NotNull(saved);
            Assert.NotEqual(0, saved.Id);
            Assert.Equal(saved.Id, newStat.Id);
            Assert.Equal(saved.Name, newStat.Name);
        }

        [Fact]
        public async Task CanGetStat()
        {
            StatDTO newStat = new StatDTO()
            {
                Id = 4,
                Name = "Agility"
            };
            var repo = BuildRepo();

            var saved = await repo.Create(newStat);

            var result = await repo.GetStat(newStat.Id);

            Assert.Equal(newStat.Id, result.Id);
        }

        [Fact]
        public async Task CanGetAllStats()
        {
            var repo = BuildRepo();

            var result = await repo.GetStats();

            // Three from seeded data
            Assert.Equal(6, result.Count);
        }

        [Fact]
        public async Task CanUpdateStat()
        {
            StatDTO newStat = new StatDTO()
            {
                Id = 1,
                Name = "StatUpdate"
            };
            var repo = BuildRepo();

            await repo.Update(newStat);

            var result = await repo.GetStat(1);

            Assert.Equal(1, result.Id);
            Assert.Equal(newStat.Name, result.Name);
        }

        [Fact]
        public async Task CanDeleteStat()
        {
            var repo = BuildRepo();

            await repo.Delete(1);

            var count = await repo.GetStats();

            Assert.Equal(5, count.Count);
        }
    }
}