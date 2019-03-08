using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
namespace webcore001.Respostory
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
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

            return await db.ExecuteAsync(sql, new { val })>0;
        }
    }
}
