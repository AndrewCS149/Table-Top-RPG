using Crypts_And_Coders.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using static Crypts_And_Coders.Models.SpeciesAndClass;

namespace Crypts_And_Coders.Data
{
    public class CryptsDbContext : DbContext
    {
        public DbSet<Character> Character { get; set; }
        public DbSet<CharacterInventory> CharacterInventory { get; set; }
        public DbSet<Weapon> Weapon { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Enemy> Enemy { get; set; }
        public DbSet<Location> Location { get; set; }

        public CryptsDbContext(DbContextOptions<CryptsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CharacterInventory>().HasKey(x => new { x.CharacterId, x.ItemId });
            modelBuilder.Entity<EnemyInLocation>().HasKey(x => new { x.LocationId, x.EnemyId });

            // seed data
            modelBuilder.Entity<Character>().HasData(
                new Character
                {
                    Id = 1,
                    Name = "Galdifor",
                    Species = Species.Elf,
                    Class = Class.Thief,
                    WeaponId = 1,
                    LocationId = 1

                },

                new Character
                {
                    Id = 2,
                    Name = "Dragorn",
                    Species = Species.Dwarf,
                    Class = Class.Paladin,
                    WeaponId = 1,
                    LocationId = 1
                },

                new Character
                {
                    Id = 3,
                    Name = "Glen",
                    Species = Species.Human,
                    Class = Class.Bard,
                    WeaponId = 1,
                    LocationId = 1
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

            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = 1,
                    Name = "Health Potion",
                    Value = 25
                },

                new Item
                {
                    Id = 2,
                    Name = "Cup",
                    Value = 5
                },

                new Item
                {
                    Id = 3,
                    Name = "Dungeon Key",
                    Value = 100
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
                    Description = "Plagued by the great war, Murkden remains uninhibited from all intelligent life forms, although various beasts still dwell in the deep marshes."
                },

                new Location
                {
                    Id = 3,
                    Name = "Lyderton",
                    Description = "Lyderton is full of simpletons who prefer to keep war and conflict outside of their borders. It is rich farmland with dense amounts of beautiful wildlife."
                }
            );

            modelBuilder.Entity<Weapon>().HasData(
                new Weapon
                {
                    Id = 1,
                    Name = "Claymore",
                    Type = "Close Range",
                    BaseDamage = 15
                },

                new Weapon
                {
                    Id = 2,
                    Name = "Wizard Staff",
                    Type = "Magical",
                    BaseDamage = 18
                },

                new Weapon
                {
                    Id = 3,
                    Name = "Longbow",
                    Type = "Long Range",
                    BaseDamage = 10
                }
            );
        }
    }
}