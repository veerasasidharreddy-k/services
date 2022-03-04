using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LightHouseMRO.AdminService.Core.Data;
using LightHouseMRO.AdminService.Domain.Entities;

namespace LightHouseMRO.AdminService.Infrastructure.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDbTransaction _dbTransaction;
        private readonly IDbConnection _dbConnection;
        private readonly string _insertRowRetrievalQuery;

        public ClientRepository(IDbTransaction dbTransaction, string insertRowRetrievalQuery)
        {
            _dbTransaction = dbTransaction;
            _dbConnection = _dbTransaction.Connection;
            _insertRowRetrievalQuery = insertRowRetrievalQuery;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync(CancellationToken cancellationToken)
        {
            var getClientsCommand = new CommandDefinition(@"SELECT * FROM Clients",
                _dbTransaction,
                cancellationToken : cancellationToken);
            return (await _dbConnection.QueryAsync<Client>(getClientsCommand))
                .ToList();
        }

        public async Task<int> CreateClientAsync(Client client, CancellationToken cancellationToken)
        {
            var insertClientSql = new StringBuilder(@"INSERT INTO Clients (Name, ModifiedBy, ModifiedAt)
                                VALUES (@Name, @ModifiedBy, @ModifiedAt)");

            var createClientCommand = new CommandDefinition(insertClientSql.Append(_insertRowRetrievalQuery).ToString(),
                new
                {
                    client.Name,
                    client.ModifiedBy,
                    client.ModifiedAt
                },
                _dbTransaction,
                cancellationToken: cancellationToken
                );

            // Let's insert the beer and grab its ID
            var clientId = await _dbConnection.ExecuteScalarAsync<int>(createClientCommand);

            return clientId;
        }

        public async Task<Client> GetClientAsync(int id, CancellationToken cancellationToken)
        {
            var getClientCommand = new CommandDefinition(@"SELECT * FROM Clients WHERE Id = @Id",
                        new { Id = id },
                        _dbTransaction,
                        cancellationToken: cancellationToken);
#pragma warning disable CS8603 // Possible null reference return.
            return (await _dbConnection.QueryAsync<Client>(getClientCommand)).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task UpdateClientAsync(Client client, CancellationToken cancellationToken)
        {
            var updateClientCommand = new CommandDefinition(
                @"UPDATE Beers SET Name = @Name WHERE Id = @Id",
                new
                {
                    Name = client.Name,
                    Id = client.Id
                },
                _dbTransaction,
                cancellationToken: cancellationToken);

            await _dbConnection.ExecuteAsync(updateClientCommand);
        }
    }
}
