using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
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
        private IUnitOfWork<Employer> employerServices;
        /// <summary>
        /// Employer controller constructor 
        /// </summary>
        /// <param name="_employerServices"></param>
        public EmployerController(IUnitOfWork<Employer> _employerServices)
        {
            employerServices = _employerServices;
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
                return Ok(employerServices.EmployerRepository.GetAll());
            }
            catch (Exception ex)
            {
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
                return  Ok(employerServices.EmployerRepository.GetById(id));
            }
            catch (Exception ex)
            {
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
                employerServices.EmployerRepository.Create(employer);
                employerServices.Commit();
                return Ok(employer);
            }
            catch (Exception ex)
            {
                employerServices.Rollback();
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
                var emp = employerServices.EmployerRepository.Update(employer);
                employerServices.Commit();
                return Ok(emp);
            }
            catch (Exception ex)
            {
                employerServices.Rollback();
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
                employerServices.EmployerRepository.Delete(id);
                employerServices.Commit();
                return Ok();
            }
            catch (Exception )
            {
                employerServices.Rollback();
                return new BadRequestResult();
            }
        }
    }
}
