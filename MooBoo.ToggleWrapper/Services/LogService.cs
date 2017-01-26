using System;
using Toggl;
using Toggl.Extensions;
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
        private readonly LogBuffer _logBuffer = new LogBuffer();
        private readonly string _token;

        public LogService(string token) : base(token)
        {
            _token = token;
            Scheduler.Instance.Schedule("syncToggle", TimeSpan.FromSeconds(5), Flush);
        }
        
        public void Add(string Name, DateTime start, DateTime stop, bool IsGood)
        {
            _logBuffer.Add(new Log
            {
                Name = Name, 
                Start = start,
                Stop = stop
            });
        }

        public string GetToken()
        {
            return _token;
        }

        public void Add(Log log)
        {
            _logBuffer.Add(log);
        }

        private void Flush()
        {
            if (_logBuffer.IsEmpty) return;

            Scheduler.Instance.Schedule("request", TimeSpan.FromSeconds(1), () =>
            {
                var entity = _logBuffer.Pop();
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
                
                if (_logBuffer.IsEmpty) Scheduler.Instance.Unschedule("request");
            });
        }
  
    }
}
