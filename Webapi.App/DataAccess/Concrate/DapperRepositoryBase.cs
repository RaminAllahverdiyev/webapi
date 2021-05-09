using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using DataAccess.Abstract;
using Dommel;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.Concrate
{
    public  class DapperRepositoryBase<T> : IRepository<T>
    {

        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } set { } }

        public DapperRepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction; 
        }
    }
}
