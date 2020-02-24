using Microsoft.AspNetCore.Mvc;
using SYF.Infrastructure.Models;
using SYF.Infrastructure.Models.Requests;
using SYF.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SYF.Web.Controllers
{
    [Route("api/timesheets")]
    public class TimesheetsController : Controller
    {
        public TimesheetsController(ITimesheetService timesheetService)
        {
            TimesheetService = timesheetService;
        }

        public ITimesheetService TimesheetService { get; set; }

        [HttpGet]
        public Task<IEnumerable<TimesheetModel>> SearchAsync([FromQuery] TimesheetSearchRequest request)
        {
            return TimesheetService.SearchAsync(request);
        }

        [HttpPost]
        public Task SaveAsync([FromBody] IEnumerable<TimesheetModel> entries)
        {
            return TimesheetService.SaveAsync(entries);
        }
    }
}
