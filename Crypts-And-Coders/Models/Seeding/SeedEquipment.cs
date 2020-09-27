using Crypts_And_Coders.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;

namespace Crypts_And_Coders.Models.EquipmentSeeding
{
    public class SeedEquipment
    {
        /// <summary>
        /// Seed equipment from the DnD API into the Weapons and Items tables
        /// </summary>
        /// <param name="serviceProvider">Current service provider info</param>
        public static void SeedEquipmentToDB(IServiceProvider serviceProvider)
        {
            using (var context = new CryptsDbContext(serviceProvider.GetRequiredService<DbContextOptions<CryptsDbContext>>()))
            {
                context.Database.EnsureCreated();
                //if (context.Item.Any())
                //{
                //}
                if (context.Item.ToList().Count > 20 && context.Weapon.ToList().Count > 20) return;
                GetAndDeserializeEquipment(context);
            }
        }

        /// <summary>
        /// Call for equipment list, then call each item in list to get specific information and pass into SeedData
        /// </summary>
        /// <param name="context">DB Context</param>
        public static void GetAndDeserializeEquipment(CryptsDbContext context)
        {
            using (WebClient wc = new WebClient())
            {
                var client = new WebClient();

                // 1st API call to get list of all items
                var response = client.DownloadString("https://www.dnd5eapi.co/api/equipment");

                var deserialized = JsonConvert.DeserializeObject<DeserializeInital.Root>(response);

                foreach (var item in deserialized.results)
                {
                    // 2nd API call to get details about specific items
                    var innerResponse = client.DownloadString($"https://www.dnd5eapi.co/api/equipment/{item.index}");

                    var innerDeserialize = JsonConvert.DeserializeObject<Root>(innerResponse);

                    SeedData(innerDeserialize, context);
                }
            }
        }

        /// <summary>
        /// Handle the data seeding
        /// </summary>
        /// <param name="deserialized">Data from API</param>
        /// <param name="context">DB Context</param>
        public static void SeedData(Root deserialized, CryptsDbContext context)
        {
            if (deserialized.weapon_category == "" || deserialized.weapon_category == null)
            {
                Item newItem = new Item
                {
                    Name = deserialized.name,
                    Value = $"{deserialized.cost.quantity} {deserialized.cost.unit}",
                };
                context.Entry(newItem).State = EntityState.Added;
                context.SaveChanges();
            }
            else
            {
                Weapon newWeapon = new Weapon
                {
                    BaseDamage = deserialized.damage != null ? deserialized.damage.damage_dice : "1d6",
                    Name = deserialized.name,
                    Type = deserialized.weapon_category
                };
                context.Entry(newWeapon).State = EntityState.Added;
                context.SaveChanges();
            }
        }
    }
}