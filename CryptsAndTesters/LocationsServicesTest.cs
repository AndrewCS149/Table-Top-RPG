using Crypts_And_Coders.Models.DTOs;
using Crypts_And_Coders.Models.Interfaces;
using Crypts_And_Coders.Models.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CryptsAndTesters
{
    public class LocationsServicesTest : DatabaseTest
    {
        private ILocation BuildRepo()
        {
            return new LocationsRepository(_db, _enemy);
        }

        [Fact]
        public async void CanCreateLocation()
        {
            LocationDTO location = new LocationDTO()
            {
                Id = 4,
                Name = "Faldor",
                Description = "Occupied by the forces of evil, Faldor consists of open, hilly plains that separate it's eastern border with towering mountains."
            };

            var repository = BuildRepo();

            var saved = await repository.Create(location);

            Assert.NotNull(saved);
            Assert.Equal(location.Name, saved.Name);
            Assert.Equal(location.Description, saved.Description);
        }

        [Fact]
        public async Task CanSaveLocation()
        {
            LocationDTO location = new LocationDTO()
            {
                Id = 4,
                Name = "Faldor",
                Description = "Occupied by the forces of evil, Faldor consists of open, hilly plains that separate it's eastern border with towering mountains."
            };
            var repo = BuildRepo();

            var saved = await repo.Create(location);

            Assert.NotNull(saved);
            Assert.NotEqual(0, saved.Id);
            Assert.Equal(saved.Id, location.Id);
            Assert.Equal(saved.Name, location.Name);
            Assert.Equal(saved.Description, location.Description);
        }

        [Fact]
        public async Task CanReadLocations()
        {
            LocationDTO location = new LocationDTO()
            {
                Id = 4,
                Name = "Faldor",
                Description = "Occupied by the forces of evil, Faldor consists of open, hilly plains that separate it's eastern border with towering mountains."
            };

            var repo = BuildRepo();

            var saved = await repo.Create(location);

            var result = await repo.GetLocation(location.Id);

            Assert.Equal(location.Id, result.Id);
        }

        [Fact]
        public async Task ReadAllLocations()
        {
            var repo = BuildRepo();

            var result = await repo.GetLocations();

            // Three from seeded data
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task CanUpdateLocations()
        {
            LocationDTO location = new LocationDTO()
            {
                Id = 1,
                Name = "Whatever",
                Description = "Your mom goes to college."
            };
            var repo = BuildRepo();

            await repo.Update(location);

            var result = await repo.GetLocation(1);

            Assert.Equal(1, result.Id);
            Assert.Equal(result.Id, location.Id);
            Assert.Equal(result.Name, location.Name);
            Assert.Equal(result.Description, location.Description);
        }

        [Fact]
        public async void CanDeleteLocations()
        {
            var repository = BuildRepo();

            await repository.Delete(1);
            var returnFromMethod = await repository.GetLocations();

            var expected = new List<string>()
            {
                "Murkden", "Plagued by the great war, Murkden remains uninhabited from all intelligent life forms, although various beasts still dwell in the deep marshes.", "Lyderton", "Lyderton is full of simpletons who prefer to keep war and conflict outside of their borders. It is rich farmland with dense amounts of beautiful wildlife."
            };

            var returnList = new List<string>();

            foreach (var item in returnFromMethod)
            {
                returnList.Add(item.Name);
                returnList.Add(item.Description);
            }

            Assert.NotNull(returnFromMethod);
            Assert.Equal(expected, returnList);
        }
    }
}