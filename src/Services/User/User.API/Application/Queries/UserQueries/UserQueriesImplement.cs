using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using User.API.Application.Queries.IUserQueries;
using User.Domain.AggregatesModel.UserAggregates;
using User.Domain.AggregatesModel.UserAggregates.Entitys;

namespace User.API.Application.Queries.UserQueries
{
    public class UserQueriesImplement : IUserQueriesInterface
    {

        private string _connectionString = string.Empty;

        public UserQueriesImplement(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }
        public async Task<IEnumerable<UserInformation>> GetOrderAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<UserInformation>(
                   @"select * from userinfo 
                        WHERE o.Id=@id"
                        , new { id }
                    );

                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();

                return result;
            }
        }
    }
}
