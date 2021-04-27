using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUnitOfWork<T>: IDisposable
    {
        IUserRepository UserRepository { get; }
        IEmployerRepository EmployerRepository { get; }

        void Commit();
        void Rollback();
    }
}
