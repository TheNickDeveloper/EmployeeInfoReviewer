using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EmployeeDataAccessLibrary.DataAccess;
using EmployeeDataAccessLibrary.Models;
using EmployeeInfoReviewer.Services;
using EmployeeInfoReviewer.Interfaces;

namespace EmployeeInfoReviewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;

        public PeopleController(PeopleContext context)
        {
            _peopleService = new PeopleService(context);
        }

        // GET: api/People
        [HttpGet]
        public IEnumerable<Person> GetPeople()
        {
            return _peopleService.Get();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public IActionResult GetPerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = _peopleService.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        public IActionResult PutPerson([FromRoute] int id, [FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.Id)
            {
                return BadRequest();
            }

            var executeResult = _peopleService.Update(id, person);

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
        public IActionResult PostPerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _peopleService.Post(person);

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public IActionResult DeletePerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var executeResult = _peopleService.Delete(id);

            if (!executeResult)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}