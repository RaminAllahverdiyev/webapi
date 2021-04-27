using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IServices<T>
    {
        IEnumerable<User> GetUser();
        IEnumerable<Employer> GetEmployer();
        User GetByIdUser(int id);
        Employer GetByIdEmployer(int id);
        User CreateUser(User User);
        Employer CreateEmployer(Employer employer);
        User UpdateUser(User User);
        Employer UpdateEmployer(Employer employer);
        void DeleteUser(int id);
        void DeleteEmployer(int id);


    }
}
