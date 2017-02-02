using System;
using MooBoo.Infrastructure.Interfaces.DataLayer;

namespace MooBoo.Model.DataLayer
{
    public class LogItem : DataObject
    {
        public int ApplicationId { get; set; } = -1;
        public Application Application { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public override DataObject Clone()
        {
            return new LogItem
            {
                ApplicationId = ApplicationId,
                Application = null,
                Start = Start,
                Stop = Stop,
                Id = Id
            };
        }

        public static string Token = "LOGITEM";
    }
}
