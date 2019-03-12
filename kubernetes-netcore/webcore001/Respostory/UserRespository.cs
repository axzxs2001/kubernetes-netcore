using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using StackExchange.Redis;

namespace webcore001.Respostory
{
    public class UserRepository : IUserRepository
    {

        private readonly IDatabase _db;
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            var redis = ConnectionMultiplexer.Connect("192.168.252.41:6379");
            _connectionString = connectionString;
            _db = redis.GetDatabase();
        }
        public List<User> GetUsers()
        {
            var db = new NpgsqlConnection(_connectionString);
            return db.Query<User>("select * from users").ToList();
        }

        public async Task<bool> AddVal(string val)
        {
            var db = new NpgsqlConnection(_connectionString);
            var sql = $@"INSERT INTO addtable(val) VALUES(@val)";

            return await db.ExecuteAsync(sql, new { val }) > 0;
        }

        public bool AddRedisVal(string val)
        {

            _db.StringSet(Guid.NewGuid().ToString(), val);
            return true;
        }
    }
}
