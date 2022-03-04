// Licensed to the AT Kearney under one or more agreements.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightHouseMRO.AdminService.Core.Data;
using LightHouseMRO.AdminService.Infrastructure.Data.Repositories;
using Microsoft.Data.SqlClient;

namespace LightHouseMRO.AdminService.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _dbTransaction;

        public IClientRepository ClientRepository { get; }

        public UnitOfWork(string connectionString)
        {
            string rowInsertRetrievalQuery = "; SELECT CAST(SCOPE_IDENTITY() as int);";

            _dbConnection = new SqlConnection(connectionString);
            _dbConnection.Open();

            _dbTransaction = _dbConnection.BeginTransaction();

            ClientRepository = new ClientRepository(_dbTransaction, rowInsertRetrievalQuery);
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not commit the transaction, reason: {e.Message}");
                _dbTransaction.Rollback();
            }
            finally
            {
                _dbTransaction.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbTransaction?.Dispose();
                _dbConnection?.Dispose();
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
