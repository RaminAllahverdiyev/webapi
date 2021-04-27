using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrate;
using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrate
{
    public class Manager<T> : IServices<T> where T : BaseEntity
    {
        public IUnitOfWork<T> unitOfWork;
        public IConfiguration configuration;
        public Manager(IConfiguration _configuration)
        {
            configuration = _configuration;
            using (var unit = new UnitOfWork<T>(configuration))
            {
                unitOfWork = unit;
            }

        }

        public User CreateUser(User entity)
        {
            return unitOfWork.UserRepository.Create(entity);
        }

        public Employer CreateEmployer(Employer entity)
        {
            return unitOfWork.EmployerRepository.Create(entity);
        }
        public void DeleteUser(int id)
        {
            unitOfWork.UserRepository.Delete(id);
        }
        public void DeleteEmployer(int id)
        {
            unitOfWork.EmployerRepository.Delete(id);
        }
        public User GetByIdUser(int id)
        {
            return unitOfWork.UserRepository.GetById(id);
        }
        public Employer GetByIdEmployer(int id)
        {
            return unitOfWork.EmployerRepository.GetById(id);
        }
        public IEnumerable<User> GetUser()
        {
            return unitOfWork.UserRepository.GetAll();
        }
        public IEnumerable<Employer> GetEmployer()
        {
            return unitOfWork.EmployerRepository.GetAll();
        }
        public User UpdateUser(User entity)
        {
            return unitOfWork.UserRepository.Update(entity);
        }
        public Employer UpdateEmployer(Employer entity)
        {
            return unitOfWork.EmployerRepository.Update(entity);
        }
    }
}
