using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        private IServices<User>  userServices;
        private readonly ILogger<UserController> logger;
        /// <summary>
        /// constructur
        /// </summary>
        /// <param name="_userServices"></param>
        /// <param name="_logger"></param>
        public UserController(IServices<User>  _userServices,ILogger<UserController> _logger)
        {
            userServices = _userServices;
            logger = _logger;
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
                logger.LogInformation("User get function");
                return Ok(userServices.GetUser());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
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
                logger.LogInformation("User id", id);
                return Ok(userServices.GetByIdUser(id));
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
                    var NewUser = userServices.CreateUser(user);
                    logger.LogInformation("User create {@user}", user);
                    return Ok(NewUser);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                    return new BadRequestObjectResult(ex.Message);
                }
            }
            else
            {
                logger.LogError("Validation erorrs");
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
                var NewUser =  userServices.UpdateUser(user);
                logger.LogInformation("Update user {@user}", user);
                return Ok(NewUser);
            }
            catch (Exception ex)
            {
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
                userServices.DeleteUser(id);
                logger.LogInformation("Delet user id @{id}", id);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return  new BadRequestResult();
            }
        }
    }
}
