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
        public Manager(IUnitOfWork<T> _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public User CreateUser(User entity)
        {
            try
            {
                unitOfWork.UserRepository.Create(entity);
                unitOfWork.Commit();
                return entity;
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
                throw new Exception();
            }
        }

        public Employer CreateEmployer(Employer entity)
        {
            try
            {
                unitOfWork.EmployerRepository.Create(entity);
                unitOfWork.Commit();
                return entity;
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
                throw new Exception();
            }
        }
        public void DeleteUser(int id)
        {
            try
            {
                unitOfWork.UserRepository.Delete(id);
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
                throw new Exception();
            }
        }
        public void DeleteEmployer(int id)
        {
            try
            {
                unitOfWork.EmployerRepository.Delete(id);
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
                throw new Exception();
            }
        }
        public User GetByIdUser(int id)
        {
            try
            {
               return unitOfWork.UserRepository.GetById(id);
            }
            catch (Exception)
            {
                throw new NullReferenceException();
            }
        }
        public Employer GetByIdEmployer(int id)
        {
            try
            {
                return unitOfWork.EmployerRepository.GetById(id);
            }
            catch (Exception)
            {
                throw new NullReferenceException();
            }
        }
        public IEnumerable<User> GetUser()
        {
            
            try
            {
                return unitOfWork.UserRepository.GetAll();
            }
            catch (Exception)
            {
                throw new NullReferenceException();
            }
        }
        public IEnumerable<Employer> GetEmployer()
        {
            try
            {
                return unitOfWork.EmployerRepository.GetAll();
            }
            catch (Exception)
            {
                throw new NullReferenceException();
            }
        }
        public User UpdateUser(User entity)
        {
            try
            {
                unitOfWork.UserRepository.Update(entity);
                unitOfWork.Commit();
                return entity;
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
                throw new ArgumentException();
            }
        }
        public Employer UpdateEmployer(Employer entity)
        {
            try
            {
                unitOfWork.EmployerRepository.Update(entity);
                unitOfWork.Commit();
                return entity;
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
                throw new ArgumentException();
            }
        }
    }
}
