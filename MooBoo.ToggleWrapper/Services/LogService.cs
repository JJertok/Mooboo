using System;
using Toggl;
using Toggl.Extensions;

namespace ToggleSandbox.Services
{
    public class LogService : BaseAPI
    {
        public LogService(string token) : base(token)
        {
        }

        public void Add(string description, DateTime start, DateTime stop, bool isGood)
        {
            var timeEntry = TimeEntryService.Add(new TimeEntry()
            {
                IsBillable = true,
                CreatedWith = "Mooboo",
                Duration = Convert.ToInt32((stop - start).TotalSeconds),
                Start = start.ToIsoDateStr(),
                Stop = stop.ToIsoDateStr(),
                WorkspaceId = DefaultWorkspaceId,
                ProjectId = isGood ? GoodProjectId : BadProjectId,
                Description = description
            });
        }
    }
}
