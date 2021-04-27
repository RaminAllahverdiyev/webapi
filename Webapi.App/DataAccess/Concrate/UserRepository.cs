using Dapper;
using DataAccess.Abstract;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DataAccess.Concrate
{
    internal class UserRepository : DapperRepositoryBase<User>, IUserRepository
    {

        public UserRepository(IDbTransaction transaction)
            : base(transaction)
        {

        }

        public  User Create(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.Id = Connection.ExecuteScalar<int>(
                "Insert into Users(Name,Surname,Age) values(@Name,@Surname,@Age); SELECT SCOPE_IDENTITY()",
                param: new { Name = entity.Name, Surname = entity.Surname, Age = entity.Age },
                transaction: Transaction
            );
            return entity;
        }

        public  void Delete(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var rowaffected=Connection.Execute(
                         "Delete from Users where id=@id;",
                         param: new { Id = id },
                         transaction: Transaction
                     );
            if (rowaffected == 0)
            {
                throw new Exception("Operation was not performed");
            }
        }

        public  IEnumerable<User> GetAll()
        {
            var users = (IEnumerable<User>)Connection.Query<User>("select * from Users", transaction: Transaction);
            if (users == null)
            {
                throw new NullReferenceException();
            }
            return users;
        }
        public  User GetById(int? ID)
        {
            if (ID == null)
            {
                throw new ArgumentNullException();
            }
            var sql = "SELECT TOP 1 * FROM Users Where id=@ID";
            var user = (User)Connection.Query<User>(
                            sql,
                            param: new { Id = ID }
                           , transaction: Transaction
                       ).SingleOrDefault();
            if (user == null)
            {
                throw new NullReferenceException("entiti not faund");
            }
            return user;
        }
        public  User Update(User entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var aff= Connection.Execute(
                "Update Users set name=@name,surname=@surname,age=@age where id=@id;",
                param: new { Id = entity.Id, Name = entity.Name, Surname = entity.Surname, Age = entity.Age },
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
