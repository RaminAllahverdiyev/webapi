using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IEmployerRepository : IRepository<Employer>
    {
        IEnumerable<Employer> GetAll();
        Employer GetById(int? id);
        Employer Create(Employer entity);
        Employer Update(Employer entity);
        void Delete(int? id);
    }
}
