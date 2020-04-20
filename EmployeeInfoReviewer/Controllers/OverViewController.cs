using System.Linq;
using EmployeeInfoReviewer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInfoReviewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OverViewController : ControllerBase
    {
        private readonly IPeopleService _peopleService;

        public OverViewController(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        // GET: api/OverView/getTotalRecordNumber
        [HttpGet("getTotalRecordNumber")]
        public int GetTotalRecordNumber()
        {
            var ppl = _peopleService.Get();
            return ppl.Count();
        }

        // GET: api/OverView/getAudltTotalRecordNumber
        [HttpGet("getAudltTotalRecordNumber")]
        public int GetAudltTotalRecordNumber()
        {
            var ppl = _peopleService.Get().Where(p => p.Age >= 18).ToList();
            return ppl.Count();
        }
    }
}
