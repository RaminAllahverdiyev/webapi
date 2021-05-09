using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetAll();
        User GetById(int? id);
        User Create(User entity);
        User Update(User entity);
        void Delete(int? id);
    }
}
