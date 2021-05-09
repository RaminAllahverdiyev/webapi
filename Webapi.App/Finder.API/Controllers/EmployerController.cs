using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finder.API.Controllers
{
    /// <summary>
    /// Employer controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : Controller
    {
        private IServices<Employer> employerServices;
        private readonly ILogger logger;
        /// <summary>
        /// Employer controller constructor 
        /// </summary>
        /// <param name="_employerServices"></param>
        public EmployerController(IServices<Employer> _employerServices, ILogger _logger)
        {
            employerServices = _employerServices;
            logger = _logger;
        }
        /// <summary>
        /// Get all employer list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult  Get()
        {
            try
            {
                logger.LogInformation("Get all employers");
                return Ok(employerServices.GetEmployer());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }
        /// <summary>
        /// Get employer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                logger.LogInformation("Get employer {@id}",id);
                return  Ok(employerServices.GetByIdEmployer(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Create employer
        /// </summary>
        /// <param name="employer"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Employer employer)
        {
            try
            {
                employerServices.CreateEmployer(employer);
                logger.LogInformation("Create new {@employer}",employer);
                return Ok(employer);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }
        /// <summary>
        /// Update employer
        /// </summary>
        /// <param name="employer"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update([FromBody] Employer employer)
        {
            try
            {
                var emp = employerServices.UpdateEmployer(employer);
                logger.LogInformation("Update {@employer}", emp);
                return Ok(emp);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Delete employer by id
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployer(int id)
        {
            try
            {
                employerServices.DeleteEmployer(id);
                logger.LogInformation("Delete employer {@id}", id);
                return Ok();
            }
            catch (Exception ex )
            {
                logger.LogError(ex.Message);
                return new BadRequestResult();
            }
        }
    }
}
