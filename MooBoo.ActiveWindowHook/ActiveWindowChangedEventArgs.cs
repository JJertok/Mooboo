using System;

namespace MooBoo.ActiveWindowHook
{
    public class ActiveWindowChangedEventArgs : EventArgs
    {

        #region Cunstructor

        public ActiveWindowChangedEventArgs(string fileName)
        {
            ProcessFileName = fileName;
        }

        #endregion

        #region Fields

        public string ProcessFileName { get; private set; }
        
        #endregion
    }
}
