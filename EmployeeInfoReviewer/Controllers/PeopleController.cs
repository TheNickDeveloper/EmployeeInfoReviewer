using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EmployeeDataAccessLibrary.Models;
using EmployeeInfoReviewer.Services;
using EmployeeInfoReviewer.Interfaces;
using Microsoft.Extensions.Configuration;
using EmployeeDataAccessLibrary.DataAccess;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeDataAccessLibrary.DataAccess.Sql;

namespace EmployeeInfoReviewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;

        //SqlServer
        public PeopleController(SqlServerPeopleContext context)
        {
            _peopleService = new PeopleService(context);
        }

        //Sqlite
        //public PeopleController(SqlitePeopleContext context)
        //{
        //    _peopleService = new PeopleService(context);
        //}

        //// MongoDB
        //public PeopleController(IConfiguration iconfig)
        //{
        //    _peopleService = new MgPeopleService(iconfig);
        //}

        // GET: api/People
        [HttpGet]
        public async Task<IEnumerable<Person>> GetPeople()
        {
            var task = Task.Run(() => _peopleService.Get());
            return await task;
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = Task.Run(() => _peopleService.Get(id));
            var person = await task;

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson([FromRoute] int id, [FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.Id)
            {
                return BadRequest();
            }

            var task = Task.Run(() => _peopleService.Update(id, person));
            var executeResult = await task;

            switch (executeResult)
            {
                case "Success":
                    return Ok();
                case "NotFound":
                    return NotFound();
                default:
                    return ValidationProblem();
            }
        }

        // POST: api/People
        [HttpPost]
        public async Task<IActionResult> PostPerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = Task.Run(() => _peopleService.Post(person));
            await task;

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = Task.Run(() => _peopleService.Delete(id));
            var executeResult = await task;

            if (!executeResult)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}