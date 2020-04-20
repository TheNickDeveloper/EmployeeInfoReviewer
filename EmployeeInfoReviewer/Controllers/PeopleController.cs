using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using EmployeeInfoReviewer.Interfaces;
using System.Threading.Tasks;
using EmployeeInfoReviewer.Models;

namespace EmployeeInfoReviewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        // GET: api/People
        [HttpGet]
        public async Task<IEnumerable<ReviewerPerson>> GetPeople()
        {
            var task = Task.Run(() => _peopleService.Get());
            var result = await task;
            return result;
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson([FromRoute] int id)
        {
            var targetPersonId = id.ToString();

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
        public async Task<IActionResult> PutPerson([FromRoute] int id, [FromBody] ReviewerPerson person)
        {
            var updatePersonId = id.ToString();

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
        public async Task<IActionResult> PostPerson([FromBody] ReviewerPerson person)
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
            var deleteePersonId = id.ToString();

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