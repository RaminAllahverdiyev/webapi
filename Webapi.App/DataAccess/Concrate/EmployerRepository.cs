using Dapper;
using DataAccess.Abstract;
using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccess.Concrate
{
    internal class EmployerRepository : DapperRepositoryBase<Employer>, IEmployerRepository
    {
        public EmployerRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }
        public  Employer Create(Employer entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.Id = Connection.ExecuteScalar<int>(
                "Insert into Employers (Name,UserId) values(@Name,@UserId); SELECT SCOPE_IDENTITY()",
                param: new { Name = entity.Name, UserId = entity.UserId },
                transaction: Transaction
            );
            return entity;
        }
        public  void Delete(int? id)
        {
          var rowaffected =  Connection.Execute(
                        "Delete from Employers where id=@id;",
                        param: new { Id = id },
                        transaction: Transaction
                    );
            if (rowaffected == 0)
            {
                throw new Exception("Operation was not performed");
            }
        }
        public  IEnumerable<Employer> GetAll()
        {
            var employers= (IEnumerable<Employer>)Connection.Query<Employer>(
                "select * from Employers", 
                transaction: Transaction
                );
            if (employers==null)
            {
                throw new NullReferenceException();
            }
            return employers;
        }
        public  Employer GetById(int? ID)
        {
            if (ID==null)
            {
                throw new ArgumentNullException();
            }

            var sql = "SELECT TOP 1 * FROM Employers Where id=@ID";
            var em= (Employer)Connection.Query<Employer>(
                            sql, 
                            param: new { Id = ID }
                           , transaction: Transaction
                       ).SingleOrDefault();
            if (em==null)
            {
                throw new NullReferenceException("entiti not faund");
            }
            return em;
        }
        public  Employer Update(Employer entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
           
               var aff= Connection.Execute(
                "Update Employers set name=@name,UserId=@UserId where id=@id;",
                param: new { Id = entity.Id, Name = entity.Name, UserId = entity.UserId },
                transaction: Transaction
            );
            if (aff == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return entity;
        }
    }
}
