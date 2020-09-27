# Application Endpoints

## Contents
- [Account](#Account)
- [Characters](#Characters)
  - [Stats](#Character-Stats)
  - [Items](#Character-Items)
- [Enemies](#Enemies)
  - [Loot](#Enemy-Loot)
- [Locations](#Locations)
  - [Enemies in Location](#Enemies-In-Location)
- [Items](#Items)
- [Weapons](#Weapons)

## Base Route
**https://table-top-rpg.azurewebsites.net/api**


### Account
*/Account/Register*

**Authorization** - GM only

**Post** - Create a new account by supplying at least a UserName, Password (Must be over 8 characters and contain a upper and lowercase letters, numbers and a special symbol) and Role, JWT token returned to use for authorization.

*In: *
```
{
    "UserName": "TestPlayer",
    "Password": "@Test1234!",
    "Email": "Dummy@email.com",
    "Role": "Player"
}
```


*/Account/Login* 

**Authorization** - Allow anonymous

**Post** - Login to an existing account by supplying the UserName and Password, JWT token returned to use for authorization.

*In: *
```json
{
    "UserName" : "TestPlayer",
    "Password" : "@Test1234!"
}
```

*Both Out:*
```json
{
    "jwt": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJSZWRoYXdrIiwianRpIjoiYTBiNzZlYjgtMTVhZS00YWM2LWIxZDctODI1MDJjMzNlYTFmIiwiVXNlck5hbWUiOiJSZWRoYXdrIiwiVXNlcklkIjoiZTYzN2Q0NGEtYTRjMC00OWZmLWE2MWYtZjc4YzAwNWIwNDliIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiR2FtZSBNYXN0ZXIiLCJleHAiOjE1OTY3MjUzODc0RCIn0.TQgDjMP-5PrSfm5B3fYo1bU",
    "expiration": "2020-08-06T14:49:45Z"
}
```

---
### Characters
*/Characters*

**Authorization** - GM only

**Get** - All characters and their information (including Inventory, Stats and Weapon)

**Post** - Create new character by supplying: WeaponId, Current Location ID, Name, Species, and Class


*/Characters/\{id\}* 

**Authorization** - GM can do anything, users can update and view their own character

**Get** - Character with the given name and their associated information.

**Put** - Update character info

**Delete** - Delete a character from the database

**Samples**

*In:*
```json
{
    "id": 1,
    "name": "Galdifor",
    "species": "Elf",
    "class": "Thief",
    "weaponId": 1,
    "locationId": 1
}
```
*Out:*
```json
{
    "id": 1,
    "name": "Galdifor",
    "species": "Elf",
    "class": "Rogue",
    "weaponId": 1,
    "weapon": {
        "id": 1,
        "name": "Claymore",
        "type": "Close Range",
        "baseDamage": "1d4"
    },
    "locationId": 1,
    "currentLocation": {
        "id": 1,
        "name": "Faldor",
        "description": "Occupied by the forces of evil, Faldor consists of open, hilly plains that separate it's eastern border with towering mountains.",
        "enemies": [
            {
                "id": 1,
                "abilities": "Slash",
                "type": "Warrior",
                "species": "Goblin"
            },
            {
                "id": 2,
                "abilities": "Smash",
                "type": "Beast",
                "species": "Troll"
            }
        ]
    },
    "inventory": [
        {
            "characterId": 1,
            "itemId": 1,
            "item": {
                "id": 1,
                "name": "Health Potion PUT",
                "value": "25"
            }
        },
        {
            "characterId": 1,
            "itemId": 2,
            "item": {
                "id": 2,
                "name": "Cup",
                "value": "5 gp"
            }
        }
    ],
    "statSheet": [
        {
            "statId": 1,
            "characterId": 1,
            "level": 5,
            "stat": {
                "name": "Charisma",
                "id": 1
            }
        },
        {
            "statId": 6,
            "characterId": 1,
            "level": 8,
            "stat": {
                "name": "Wisdom",
                "id": 6
            }
        }
    ],
    "userName": "Seed"
}

```
---
### Character Stats
*Character/\{CharacterId\}/Stats* 

**Authorization** - Allow anonymous for all get, DM only for post

**Get** - All of a specific character's stats

**Post** - Create new character by supplying: Stat, Character, and Level

*Character/\{CharacterId\}/Stats/\{StatId\}* 

**Authorization** - Allow anonymous for all get specific Character Stats, DM only for put

**Get** - One of a specific character's stats

**Put** - Update CharacterStats info

**Delete** - Deletes Character stat from database

**Samples**

*In:*

```json
{
    "statId": 1,
    "characterId": 1,
    "level": 8,
}
```
*Out:*
```json
{
    "statId": 1,
    "characterId": 1,
    "level": 8,
    "stat": {
        "name": "Stealth",
        "id": 1
    }
}
```
---
### Character Items

*Character/\{CharacterId\}/Items/\{ItemId\}* 

**Authorization** - Character's User and GM only

**Post** - Add item with given id to character's inventory.

**Delete** - Delete item from character's inventory.


---
### Enemies
*/Enemies*

**Authorization** - Get allows anonymous, Post is GM only

**Get** - All Enemies and their information

**Post** - Create new enemy by supplying: Name and Description


*/Enemies/\{EnemyId\}* 

**Authorization** - Get allows anonymous, Put & Delete are GM only

**Get** - Enemy with the given id and their associated information.

**Put** - Update enemy info

**Delete** - Delete enemy from database


**Samples**

*In:*
```json
{
    "id": 1,
    "abilities": "Smack",
    "type": "Warrior",
    "species": "Goblin"
}
```
*Out:*
```json
{
    "id": 1,
    "abilities": "Smack",
    "type": "Warrior",
    "species": "Goblin",
    "loot": [
        {
            "enemyId": 1,
            "itemId": 1,
            "item": {
                "id": 1,
                "name": "Health Potion",
                "value": 25
            }
        },
        {
            "enemyId": 1,
            "itemId": 2,
            "item": {
                "id": 2,
                "name": "Cup",
                "value": 5
            }
        }
    ]
}
```

---
### Enemy Loot

*Enemy/\{EnemyId\}/Items/\{ItemId\}* 

**Authorization** - GM only

**Post** - Add item with given id to enemy's loot.

**Delete** - Delete item from enemy's loot.

---
### Locations
*/Locations*

**Authorization** - GM only

**Get** - All Locations and their information (characters and enemies in location)

**Post** - Create new location by supplying: Name and Description


*/Locations/\{LocationId\}* 

**Authorization** - GM Only

**Get** - Location with the given name and their associated information.

**Put** - Update location info

**Delete** - Deletes a location from the database

**Samples**

*In:*
```json
{
    "id": 1,
    "name": "Falador",
    "description": "Occupied by the forces of evil, Falador consists of open, hilly plains that separate it's eastern border with towering mountains."
}
```
*Out:*
```json
{
    "id": 1,
    "name": "FaldorPUT",
    "description": "Occupied by the forces of evil, Faldor consists of open, hilly plains that separate it's eastern border with towering mountains.",
    "enemies": [
        {
            "id": 1,
            "abilities": "PUT",
            "type": "Warrior",
            "species": "Goblin"
        },
        {
            "id": 2,
            "abilities": "Smash",
            "type": "Beast",
            "species": "Troll"
        }
    ]
}
```

---
### Enemies in Location

*Location/\{LocationId\}/Enemies/\{EnemyId\}* 

**Authorization** - GM only

**Post** - Add enemy to a location.

**Delete** - Remove enemy from location.

---

### Items
*/Items*

**Authorization** - Allow anonymous for all gets, DM only for post

**Get** - All Items and their information

**Post** - Create new item by supplying: Name and Value


*/Items/\{ItemId\}* 

**Authorization** - Allow anonymous for all gets, DM only for put

**Get** - Item with the given id and it's associated information.

**Put** - Update item info

**Delete** - Delete an item from the database

**Samples**

*In/Out:*
```json
{
    "id": 1,
    "name": "Health Potion",
    "value": 25
}
```

---

### Weapons
*/Weapons*

**Authorization** - Allow anonymous for all get, DM only for post

**Get** - All Weapon items and their information

**Post** - Create new weapon by supplying: Name and type

*/Weapons/\{WeaponId\}* 

**Authorization** - Allow anonymous for all gets, DM only for put

**Get** - Weapon with the given id and it's associated information.

**Put** - Update weapon info

**Delete** - Delete a weapon from the database

**Samples**

*In/Out:*
```json
{
    "id": 1,
    "name": "Dagger",
    "type": "Close Range",
    "baseDamage": 15
}
```