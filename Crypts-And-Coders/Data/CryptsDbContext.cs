using Crypts_And_Coders.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Crypts_And_Coders.Models.SpeciesAndClass;

namespace Crypts_And_Coders.Data
{
    public class CryptsDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Character> Character { get; set; }
        public DbSet<CharacterInventory> CharacterInventory { get; set; }
        public DbSet<Weapon> Weapon { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Enemy> Enemy { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<EnemyInLocation> EnemyInLocation { get; set; }
        public DbSet<Stat> Stat { get; set; }
        public DbSet<CharacterStat> StatSheet { get; set; }
        public DbSet<EnemyLoot> EnemyLoot { get; set; }
        public DbSet<LogData> Logs { get; set; }

        public CryptsDbContext(DbContextOptions<CryptsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CharacterInventory>().HasKey(x => new { x.CharacterId, x.ItemId });
            modelBuilder.Entity<EnemyInLocation>().HasKey(x => new { x.LocationId, x.EnemyId });
            modelBuilder.Entity<CharacterStat>().HasKey(x => new { x.StatId, x.CharacterId });
            modelBuilder.Entity<EnemyLoot>().HasKey(x => new { x.EnemyId, x.ItemId });

            #region DataSeeding

            // seed data
            modelBuilder.Entity<Character>().HasData(
                new Character
                {
                    Id = 1,
                    Name = "Galdifor",
                    Species = Species.Elf,
                    Class = Class.Rogue,
                    WeaponId = 1,
                    LocationId = 1,
                    UserName = "Seed"
                },

                new Character
                {
                    Id = 2,
                    Name = "Dragorn",
                    Species = Species.Dwarf,
                    Class = Class.Paladin,
                    WeaponId = 2,
                    LocationId = 2,
                    UserName = "Seed"
                },

                new Character
                {
                    Id = 3,
                    Name = "Glen",
                    Species = Species.Human,
                    Class = Class.Bard,
                    WeaponId = 3,
                    LocationId = 3,
                    UserName = "Seed"
                }
            );

            modelBuilder.Entity<Enemy>().HasData(
                new Enemy
                {
                    Id = 1,
                    Abilities = "Slash",
                    Type = "Warrior",
                    Species = Species.Goblin,
                },

                new Enemy
                {
                    Id = 2,
                    Abilities = "Smash",
                    Type = "Beast",
                    Species = Species.Troll,
                },

                new Enemy
                {
                    Id = 3,
                    Abilities = "Firebreath",
                    Type = "Mythical",
                    Species = Species.Dragon
                }
            );

            modelBuilder.Entity<Location>().HasData(
                new Location
                {
                    Id = 1,
                    Name = "Faldor",
                    Description = "Occupied by the forces of evil, Faldor consists of open, hilly plains that separate it's eastern border with towering mountains."
                },

                new Location
                {
                    Id = 2,
                    Name = "Murkden",
                    Description = "Plagued by the great war, Murkden remains uninhabited from all intelligent life forms, although various beasts still dwell in the deep marshes."
                },

                new Location
                {
                    Id = 3,
                    Name = "Lyderton",
                    Description = "Lyderton is full of simpletons who prefer to keep war and conflict outside of their borders. It is rich farmland with dense amounts of beautiful wildlife."
                }
            );

            modelBuilder.Entity<Stat>().HasData(
                new Stat
                {
                    Id = 1,
                    Name = "Charisma"
                },

                new Stat
                {
                    Id = 2,
                    Name = "Constitution"
                },

                new Stat
                {
                    Id = 3,
                    Name = "Dexterity"
                },

                new Stat
                {
                    Id = 4,
                    Name = "Intelligence"
                },

                new Stat
                {
                    Id = 5,
                    Name = "Strength"
                },

                new Stat
                {
                    Id = 6,
                    Name = "Wisdom"
                }
            );

            modelBuilder.Entity<CharacterStat>().HasData(
                 new CharacterStat
                 {
                     StatId = 1,
                     CharacterId = 1,
                     Level = 5
                 },

                 new CharacterStat
                 {
                     StatId = 6,
                     CharacterId = 1,
                     Level = 8
                 },

                 new CharacterStat
                 {
                     StatId = 2,
                     CharacterId = 2,
                     Level = 2
                 },

                 new CharacterStat
                 {
                     StatId = 3,
                     CharacterId = 2,
                     Level = 10
                 },

                 new CharacterStat
                 {
                     StatId = 4,
                     CharacterId = 3,
                     Level = 4
                 },

                 new CharacterStat
                 {
                     StatId = 5,
                     CharacterId = 3,
                     Level = 7
                 }
            );

            modelBuilder.Entity<EnemyInLocation>().HasData(
                 new EnemyInLocation
                 {
                     LocationId = 1,
                     EnemyId = 1
                 },

                 new EnemyInLocation
                 {
                     LocationId = 1,
                     EnemyId = 2
                 },

                 new EnemyInLocation
                 {
                     LocationId = 2,
                     EnemyId = 3
                 },

                 new EnemyInLocation
                 {
                     LocationId = 2,
                     EnemyId = 2
                 },

                 new EnemyInLocation
                 {
                     LocationId = 3,
                     EnemyId = 1
                 },

                 new EnemyInLocation
                 {
                     LocationId = 3,
                     EnemyId = 3
                 }
             );

            modelBuilder.Entity<Weapon>().HasData(
                new Weapon
                {
                    Id = 1,
                    Name = "Claymore",
                    Type = "Close Range",
                    BaseDamage = "1d4"
                },

                new Weapon
                {
                    Id = 2,
                    Name = "Wizard Staff",
                    Type = "Magical",
                    BaseDamage = "1d8"
                },

                new Weapon
                {
                    Id = 3,
                    Name = "Longbow",
                    Type = "Long Range",
                    BaseDamage = "1d6"
                }
            );

            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = 1,
                    Name = "Health Potion",
                    Value = "25 gp"
                },

                new Item
                {
                    Id = 2,
                    Name = "Cup",
                    Value = "5 gp"
                },

                new Item
                {
                    Id = 3,
                    Name = "Dungeon Key",
                    Value = "100 gp"
                }
            );

            modelBuilder.Entity<CharacterInventory>().HasData(
                new CharacterInventory
                {
                    CharacterId = 1,
                    ItemId = 1
                },

                new CharacterInventory
                {
                    CharacterId = 1,
                    ItemId = 2
                },

                new CharacterInventory
                {
                    CharacterId = 2,
                    ItemId = 2
                },

                new CharacterInventory
                {
                    CharacterId = 2,
                    ItemId = 3
                },

                new CharacterInventory
                {
                    CharacterId = 3,
                    ItemId = 1
                },

                new CharacterInventory
                {
                    CharacterId = 3,
                    ItemId = 3
                }
            );

            modelBuilder.Entity<EnemyLoot>().HasData(
                new EnemyLoot
                {
                    EnemyId = 1,
                    ItemId = 1
                },

                new EnemyLoot
                {
                    EnemyId = 1,
                    ItemId = 2
                },

                new EnemyLoot
                {
                    EnemyId = 2,
                    ItemId = 2
                },

                new EnemyLoot
                {
                    EnemyId = 2,
                    ItemId = 3
                },

                new EnemyLoot
                {
                    EnemyId = 3,
                    ItemId = 1
                },

                new EnemyLoot
                {
                    EnemyId = 3,
                    ItemId = 3
                }
            );

            #endregion DataSeeding
        }
    }
}