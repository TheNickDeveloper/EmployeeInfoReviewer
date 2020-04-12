using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EmployeeDataAccessLibrary.Models;
using EmployeeInfoReviewer.Services;
using EmployeeInfoReviewer.Interfaces;
using System.Threading.Tasks;
using EmployeeDataAccessLibrary.DataAccess.Sql;
using Microsoft.Extensions.Logging;
using System;

namespace EmployeeInfoReviewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;
        private readonly ILogger<PeopleController> _logger;
        private readonly LogHelper _logHelper = new LogHelper("PeopleControllerClass");


        //SqlServer
        public PeopleController(SqlServerPeopleContext context, ILogger<PeopleController> logger)
        {
            _peopleService = new PeopleService(context);
            _logger = logger;
            _logger.LogInformation(_logHelper.GetConnectionDb("SqlServer"));
        }

        ////Sqlite
        //public PeopleController(SqlitePeopleContext context, ILogger<PeopleController> logger)
        //{
        //    _peopleService = new PeopleService(context);
        //    _logger = logger;
        //    _logger.LogInformation(_logHelper.GetConnectionDb("Sqlite"));
        //}

        //// MongoDB
        //public PeopleController(IConfiguration iconfig, ILogger<PeopleController> logger)
        //{
        //    _peopleService = new MgPeopleService(iconfig);
        //    _logger = logger;
        //    _logger.LogInformation(_logHelper.GetConnectionDb("MongoDb"));
        //}

        // GET: api/People
        [HttpGet]
        public async Task<IEnumerable<Person>> GetPeople()
        {
            _logger.LogInformation(_logHelper.GetTaskActionName("GetPeople"));

            var task = Task.Run(() => _peopleService.Get());
            var result = await task;

            _logger.LogInformation(_logHelper.ReturnSuccessStatus());
            return result;
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson([FromRoute] int id)
        {
            var targetPersonId = id.ToString();
            _logger.LogInformation(_logHelper.GetTaskActionName("GetPerson", targetPersonId));

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Bad request at {time}", DateTime.UtcNow);
                return BadRequest(ModelState);
            }

            var task = Task.Run(() => _peopleService.Get(id));
            var person = await task;

            if (person == null)
            {
                _logger.LogWarning(_logHelper.ReturnNoFoudStatus(targetPersonId));
                return NotFound();
            }

            _logger.LogInformation(_logHelper.ReturnSuccessStatus());
            return Ok(person);
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson([FromRoute] int id, [FromBody] Person person)
        {
            var updatePersonId = id.ToString();
            _logger.LogInformation(_logHelper.GetTaskActionName("PutPerson", updatePersonId));

            if (!ModelState.IsValid)
            {
                _logger.LogWarning(_logHelper.ReturnBadRequestStatus());
                return BadRequest(ModelState);
            }

            if (id != person.Id)
            {
                _logger.LogWarning(_logHelper.ReturnBadRequestStatus());
                return BadRequest();
            }

            var task = Task.Run(() => _peopleService.Update(id, person));
            var executeResult = await task;

            switch (executeResult)
            {
                case "Success":
                    _logger.LogInformation(_logHelper.ReturnSuccessStatus());
                    return Ok();
                case "NotFound":
                    _logger.LogWarning(_logHelper.ReturnNoFoudStatus(updatePersonId));
                    return NotFound();
                default:
                    _logger.LogCritical(_logHelper.ReturnUncontrolException());
                    return ValidationProblem();
            }
        }

        // POST: api/People
        [HttpPost]
        public async Task<IActionResult> PostPerson([FromBody] Person person)
        {
            _logger.LogInformation(_logHelper.GetTaskActionName("PostPerson"));

            if (!ModelState.IsValid)
            {
                _logger.LogWarning(_logHelper.ReturnBadRequestStatus());
                return BadRequest(ModelState);
            }

            var task = Task.Run(() => _peopleService.Post(person));
            await task;

            _logger.LogInformation(_logHelper.ReturnSuccessStatus());
            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            var deleteePersonId = id.ToString();

            _logger.LogInformation(_logHelper.GetTaskActionName("DeletePerson", deleteePersonId));

            if (!ModelState.IsValid)
            {
                _logger.LogWarning(_logHelper.ReturnBadRequestStatus());
                return BadRequest(ModelState);
            }

            var task = Task.Run(() => _peopleService.Delete(id));
            var executeResult = await task;

            if (!executeResult)
            {
                _logger.LogWarning(_logHelper.ReturnNoFoudStatus(deleteePersonId));
                return NotFound();
            }

            _logger.LogInformation(_logHelper.ReturnSuccessStatus());
            return Ok();
        }

    }
}