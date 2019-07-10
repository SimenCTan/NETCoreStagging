using Dapper;
using KYExpress.Core.Domain.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace KYExpress.Dapper.Domain
{
    public class DapperQueryService : IQueryService, IDisposable
    {
        private string _connectionString;

        private IDbConnection _activeConnection;
        private IDbConnection ActiveConnection {
            get {
                if(_activeConnection == null)
                {
                    _activeConnection = DbConnectionFactory.CreateDbConnection(_connectionString);
                }
                return _activeConnection;
            }
        }
        public IDbConnectionFactory DbConnectionFactory { get; set; }

        public DapperQueryService(IDbConnectionFactory _dbConnectionFactory, string connectionString)
        {
            DbConnectionFactory = _dbConnectionFactory;
            _connectionString = connectionString;
        }

        public IEnumerable<TAny> Query<TAny>(string query, object parameters = null) where TAny : class
        {
            return ActiveConnection.Query<TAny>(query,parameters);
        }

        public TAny QueryFirst<TAny>(string query,object parameters = null)where TAny : class
        {
            return ActiveConnection.QueryFirst<TAny>(query, parameters);
        }

        public Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null) where TAny : class
        {
            return ActiveConnection.QueryAsync<TAny>(query, parameters);
        }

        public Task<TAny> QueryFirstAsync<TAny>(string query, object parameters = null) where TAny : class
        {
            return ActiveConnection.QueryFirstAsync<TAny>(query, parameters);
        }

        public void ChangeDataBase(string connectionString)
        {
            if(_activeConnection != null)
            {
                _activeConnection.Close();
                _activeConnection = null;
            }
            _connectionString = connectionString;
        }


        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ActiveConnection?.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
