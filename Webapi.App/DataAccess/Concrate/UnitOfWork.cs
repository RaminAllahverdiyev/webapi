using Dapper;
using DataAccess.Abstract;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.Concrate
{
    public class UnitOfWork<T>: IUnitOfWork<T> where T : BaseEntity
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IUserRepository _userRepository;
        private IEmployerRepository _employerRepository;
        private bool _disposed;
        public UnitOfWork(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_transaction)); }
        }

        public IEmployerRepository EmployerRepository
        {
            get { return _employerRepository ?? (_employerRepository = new EmployerRepository(_transaction)); }
        }
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _userRepository = null;
            _employerRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Rollback ()
        {
            _transaction.Rollback();
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }

    }
}
