using System.Linq;
using EmployeeInfoReviewer.Interfaces;
using EmployeeInfoReviewer.Services.LogControllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeInfoReviewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OverViewController : ControllerBase
    {
        private readonly IPeopleService _peopleService;
        private readonly ILogger<OverViewController> _logger;
        private readonly ILogHelper _logHelper;

        public OverViewController(IPeopleService peopleService, ILogger<OverViewController> logger, ILogHelper logHelper)
        {
            _peopleService = peopleService;
            _logger = logger;
            _logHelper = logHelper;
            _logHelper.ClassName = "OverViewController";
            _logHelper.ActionTaskNameHandler = new OverviewControllerLogActionNameHandler();
        }


        // GET: api/OverView/getTotalRecordNumber
        [HttpGet("getTotalRecordNumber")]
        public int GetTotalRecordNumber()
        {
            _logger.LogInformation(_logHelper.GetTaskActionName("GetTotalRecordNumber"));
            var ppl = _peopleService.Get();
            return ppl.Count();
        }

        // GET: api/OverView/getAudltTotalRecordNumber
        [HttpGet("getAudltTotalRecordNumber")]
        public int GetAudltTotalRecordNumber()
        {
            _logger.LogInformation(_logHelper.GetTaskActionName("GetAudltTotalRecordNumber"));
            var ppl = _peopleService.Get().Where(p => p.Age >= 18).ToList();
            return ppl.Count();
        }
    }
}
