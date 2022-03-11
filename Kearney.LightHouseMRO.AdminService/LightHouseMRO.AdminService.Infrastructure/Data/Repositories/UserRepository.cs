using System;
using LightHouseMRO.AdminService.Core.Data;
using Dapper;
using System.Data;

namespace LightHouseMRO.AdminService.Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbTransaction _dbTransaction;
        private readonly IDbConnection _dbConnection;
        private readonly string _insertRowRetrievalQuery;

        public UserRepository(IDbTransaction dbTransaction, string insertRowRetrievalQuery)
        {
            _dbTransaction = dbTransaction;
            _dbConnection = _dbTransaction.Connection;
            _insertRowRetrievalQuery = insertRowRetrievalQuery;
        }

        public async Task<Domain.Models.User> GetUserRolesAsync(string email, CancellationToken cancellationToken)
        {
            var procedure = "spGetUserRoles";
            var getUserRolesCommand = new CommandDefinition(procedure,
                new
                {
                    Email = email
                },
                _dbTransaction,
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken
                );
            return (await _dbConnection.QueryAsync<Domain.Models.User>(getUserRolesCommand)).FirstOrDefault();
        }
    }
}

