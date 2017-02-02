using System;

namespace MooBoo.Model
{
    public class WindowChangeEntry
    {

        #region Constructor
        public WindowChangeEntry(int applicationId, string category, DateTime switchToTime)
        {
            ApplicationId = applicationId;
            Category = category;
            SwitchToTime = switchToTime;
        }

        #endregion

        #region Properties

        public int ApplicationId { get; private set; }
        public string Category { get; private set; }
        public DateTime SwitchToTime { get; private set; }

        #endregion
    }
}
