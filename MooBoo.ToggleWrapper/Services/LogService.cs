using System;
using Mooboo.ToggleWrapper.Services;
using Toggl;
using Toggl.Extensions;
using System.Collections;
using System.Collections.Generic;
using MooBoo.Utilities;

namespace ToggleSandbox.Services
{
    public class Log
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
    }
    public class LogService : BaseAPI
    {
        private LogBuffer logBuffer = new LogBuffer();
        public LogService(string token) : base(token)
        {
            Scheduler.Instance.Schedule("syncToggle", TimeSpan.FromSeconds(5), () => { Flush(); });
        }
        
        public void Add(Log log)
        {
            logBuffer.Add(log);
        }

        private void Flush()
        {
            if (logBuffer.IsEmpty) return;

            Scheduler.Instance.Schedule("request", TimeSpan.FromSeconds(1), () =>
            {
                var entity = logBuffer.Pop();
                var attempts = 0;
                var attemptSuccess = false;
                do
                {
                    try
                    {
                        TimeEntryService.Add(new TimeEntry()
                        {
                            IsBillable = true,
                            CreatedWith = "Mooboo",
                            Duration = (int)(entity.Stop - entity.Start).TotalSeconds,
                            Start = entity.Start.ToIsoDateStr(),
                            Stop = entity.Stop.ToIsoDateStr(),
                            WorkspaceId = DefaultWorkspaceId,
                            ProjectId = GoodProjectId,
                            Description = entity.Name
                        });
                        attemptSuccess = true;
                    }
                    catch (Exception e)
                    {
                        attempts++;
                    }
                } while (attempts < 3 && !attemptSuccess);
                
                if (logBuffer.IsEmpty) Scheduler.Instance.Unschedule("request");
            });
        }


        


        //public void Add(string description, DateTime start, DateTime stop, bool isGood)
        //{
        //    logs.Add(new TimeEntry()
        //    {
        //        IsBillable = true,
        //        CreatedWith = "Mooboo",
        //        Duration = Convert.ToInt32((stop - start).TotalSeconds),
        //        Start = start.ToIsoDateStr(),
        //        Stop = stop.ToIsoDateStr(),
        //        WorkspaceId = DefaultWorkspaceId,
        //        ProjectId = isGood ? GoodProjectId : BadProjectId,
        //        Description = description
        //    });
        //}
        
    }
}
