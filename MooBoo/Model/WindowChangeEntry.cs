using System;

namespace MooBoo.Model
{
    public class WindowChangeEntry
    {

        #region Constructor
        public WindowChangeEntry(string fileName, string category, DateTime switchToTime)
        {
            FileName = fileName;
            Category = category;
            SwitchToTime = switchToTime;
        }

        #endregion

        #region Properties

        public string FileName { get; private set; }
        public string Category { get; private set; }
        public DateTime SwitchToTime { get; private set; }

        #endregion
    }
}
