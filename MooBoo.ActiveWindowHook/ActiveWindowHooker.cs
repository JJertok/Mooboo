using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MooBoo.ActiveWindowHook
{
    public class ActiveWindowHooker
    {

        #region Constants

        private const uint WINEVENT_OUTOFCONTEXT = 0;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;

        #endregion

        #region Fields

        private delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
        private WinEventDelegate dele = null;

        #endregion

        #region Constructors

        public ActiveWindowHooker()
        {
            dele = new WinEventDelegate(WinEventProc);
        }

        #endregion


        #region DllImports

        [DllImport("user32.dll")]
        private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        #endregion

        #region Methods

        public void Init()
        {
            SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, dele, 0, 0, WINEVENT_OUTOFCONTEXT);
        }

        private string GetActiveProcessFileName()
        {
            const int nChars = 256;
            IntPtr handle = IntPtr.Zero;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(handle, out pid);
            var p = Process.GetProcessById((int)pid);

            return p.MainModule.FileName.Split('\\').Last();
            //if (GetWindowText(handle, Buff, nChars) > 0)
            //{
            //    return Buff.ToString().Split('\\').Last();
            //}
            //return null;
        }

        private void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            var handler = ActiveWindowChanged;
            if (handler != null)
            {
                handler.Invoke(this, new ActiveWindowChangedEventArgs(GetActiveProcessFileName()));
            }
        }

        #endregion

        #region Events

        public event EventHandler<ActiveWindowChangedEventArgs> ActiveWindowChanged;

        #endregion
    }
}
