using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finder.API.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUnitOfWork<User>  userServices;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="_userServices"></param>
        public UserController(IUnitOfWork<User>  _userServices)
        {
            userServices = _userServices;
        }
        /// <summary>
        /// Get all users list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(userServices.UserRepository.GetAll());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(userServices.UserRepository.GetById(id));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var NewUser = userServices.UserRepository.Create(user);
                    userServices.Commit();
                    return Ok(NewUser);
                }
                catch (Exception ex)
                {
                    userServices.Rollback();
                    return new BadRequestObjectResult(ex.Message);
                }
            }
            else
            {
                throw new ValidationException("Validation erorrs");
            }            

        }
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody] User user)
        {
            try
            {
                var NewUser =  userServices.UserRepository.Update(user);
                userServices.Commit();
                return Ok(NewUser);
            }
            catch (Exception ex)
            {
                userServices.Rollback();
                throw new ArgumentOutOfRangeException(ex.Message);
            }
        }
        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {   
            try
            {
                userServices.UserRepository.Delete(id);
                userServices.Commit();
                return Ok();
            }
            catch (Exception )
            {
                userServices.Rollback();
                return  new BadRequestResult();
            }
        }
    }
}
