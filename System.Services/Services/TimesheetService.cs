using AutoMapper;
using Microsoft.Extensions.Logging;
using SYF.Data;
using SYF.Infrastructure.Models.Requests;
using SYF.Infrastructure.Models;
using SYF.Infrastructure.Providers;
using SYF.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SYF.Framework;
using SYF.Infrastructure.Criteria;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SYF.Infrastructure.Entities;

namespace SYF.Services.Services
{
    public class TimesheetService: BaseService, ITimesheetService
    {
        private readonly IMapper _mapper;

        public TimesheetService(DataContext context, IServiceProvider serviceProvider, IClaimsPrincipalProvider principalProvider, ILoggerFactory loggerFactory, IMapper mapper)
            : base(context, serviceProvider, principalProvider, loggerFactory)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<TimesheetModel>> SearchAsync(TimesheetSearchRequest request)
        {
            if (request.Periods == null) request.Periods = 7;//Always return the current weeks timesheet
            request.FromDate = DateTime.Now;
            request.FromDate = request.FromDate.StartOfWeek(DayOfWeek.Monday);


            // Build list of periods
            var periodList = new List<DateTime>();
            var currentDate = request.FromDate;
            for (int i = 0; i < request.Periods; i++)
            {
                periodList.Add(currentDate);
                currentDate = currentDate.AddDays(1);
            }

            var criteria = _mapper.Map<TimesheetCriteria>(request);
            criteria.ToDate = periodList.Last();
            criteria.Deleted = false;

            // Get all employees
            var employees = await DataContext.People
                    .AsNoTracking()
                    .Where(x => !x.Deleted)
                    .ToListAsync();

            // Build the timesheet query
            var timesheetQry = DataContext.Timesheets
                .AsNoTracking()
                .Query(criteria)
                .Where(x => !x.Deleted);

            var entries = await timesheetQry.ToListAsync();

            var results = new List<TimesheetModel>();
            foreach (var timesheetDate in periodList)
            {
                foreach (var employee in employees)
                {
                    var timesheetValues = entries.Where(a => a.TimesheetDate == timesheetDate &&
                                                          a.PersonId == employee.Id)
                                              .ToList();

                    var model = new TimesheetModel
                    {
                        PersonId = employee.Id,
                        TimesheetDate = timesheetDate
                    };

                    var entry = timesheetValues.FirstOrDefault();
                    if (entry != null)
                    {
                        model.Id = entry.Id;
                        model.TimesheetEmployeeHours = entry.TimesheetEmployeeHours;
                    }

                    results.Add(model);
                }
            }

            return results;
        }

        public async Task SaveAsync(IEnumerable<TimesheetModel> entries)
        {
            var employees = await DataContext.People.Where(x => !x.Deleted).ToListAsync();

            try
            {
                foreach (var entry in entries)
                {
                    Timesheet timesheet = null;

                    if (entry.Id == Guid.Empty)
                    {
                        // No point creating timesheet with an empty value
                        if (entry.TimesheetEmployeeHours == null) continue;
                    }
                    else
                    {
                        timesheet = await DataContext.Timesheets.FirstOrDefaultAsync(x => x.Id == entry.Id);
                    }
                    var employee = employees.FirstOrDefault(x => x.Name.Trim() == entry.PersonName.Trim());

                    // Try find the target using person id and person name
                    if (timesheet == null)
                    {
                        var criteria = new TimesheetCriteria
                        {
                            PersonId = employee.Id,
                            PersonName = employee.Name,
                            TimesheetDate = entry.TimesheetDate,
                            Deleted = false
                        };

                        timesheet = await DataContext.Timesheets.Query(criteria).FirstOrDefaultAsync();
                    }

                    // Finally create the target if we can't find it
                    if (timesheet == null)
                    {
                        timesheet = new Timesheet
                        {
                            Id = SequentialGuid.NewGuid(),
                            PersonId = employee.Id,
                            PersonName = employee.Name,
                            TimesheetDate = entry.TimesheetDate
                        };

                        DataContext.Timesheets.Add(timesheet);
                    }

                    timesheet.TimesheetEmployeeHours = entry.TimesheetEmployeeHours;
                }

                await DataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.LogError("Error saving user timesheets", ex);
            }
        }
    }
}
