using SYF.Infrastructure.Models;
using SYF.Infrastructure.Models.Requests;
using SYF.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SYF.Infrastructure.Services
{
    public interface ITimesheetService : IService
    {
        Task<IEnumerable<TimesheetModel>> SearchAsync(TimesheetSearchRequest request);
        Task SaveAsync(IEnumerable<TimesheetModel> entries);
    }
}
