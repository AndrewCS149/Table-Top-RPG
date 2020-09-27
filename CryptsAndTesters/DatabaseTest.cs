using Crypts_And_Coders.Data;
using Crypts_And_Coders.Models.Interfaces;
using Crypts_And_Coders.Models.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace CryptsAndTesters
{
    public class DatabaseTest : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly CryptsDbContext _db;
        protected readonly ICharacterStat _characterStat;
        protected readonly ICharacter _character;

        protected readonly IStat _stat;
        protected readonly ILocation _location;
        protected readonly IItem _item;
        protected readonly IWeapon _weapon;
        protected readonly IEnemy _enemy;

        public DatabaseTest()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new CryptsDbContext(
                new DbContextOptionsBuilder<CryptsDbContext>()
                .UseSqlite(_connection)
                .Options);
            _db.Database.EnsureCreated();

            _character = new CharacterRepository(_db, _characterStat, _weapon, _location);
            _characterStat = new CharacterStatRepository(_db, _stat);
            _stat = new StatRepository(_db);
            _location = new LocationsRepository(_db, _enemy);
            _item = new ItemRepository(_db);
            _weapon = new WeaponRepository(_db);
            _enemy = new EnemyRepository(_db);
        }

        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}