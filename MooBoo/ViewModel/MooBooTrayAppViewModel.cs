using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using MooBoo.ActiveWindowHook;
using MooBoo.Model;
using MooBoo.Properties;
using MooBoo.Resources;
using MooBoo.Utility;
using ToggleSandbox.Services;

namespace MooBoo.ViewModel
{
    public class MooBooTrayAppViewModel : INotifyPropertyChanged
    {
        #region Fileds


        private readonly ActiveWindowHooker windowHooker = new ActiveWindowHooker();
        private List<WindowChangeEntry> entries = new List<WindowChangeEntry>();
        private object locker = new object();
        //TODO remove this logic from here to MooBoo.ToggleWrapper
        private readonly DateTime lastToggleUpdate = new DateTime();
        //TODO remove this logic from here to MooBoo.ToggleWrapper
        private readonly LogService toggleService = new LogService("STRINGTOKEN");


        #endregion

        #region Constructor

        public MooBooTrayAppViewModel()
        {
            windowHooker.ActiveWindowChanged += OnActiveWindowChanged;
            windowHooker.Init();

            Scheduler.Instance.Schedule("ToggleServiceSender", TimeSpan.FromSeconds(20), OnSendToToggleService);
        }

        #endregion

        #region Properties

        public Icon TrayIcon
        {
            get { return Icons.TrayIcon; }
        }

        #endregion

        #region Methods

        private void OnActiveWindowChanged(object sender, ActiveWindowChangedEventArgs e)
        {
            lock (locker)
            {
                entries.Add(new WindowChangeEntry(e.ProcessFileName, "Good", DateTime.Now));
            }
        }

        //TODO remove this logic from here to MooBoo.ToggleWrapper
        //TODO redesign begin-end period calculation algorithm
        private void OnSendToToggleService()
        {
            List<WindowChangeEntry> entriesToProcess = null;
            var time = DateTime.Now;
            lock (locker)
            {
                entries = entries.Where(e => e.SwitchToTime > lastToggleUpdate).ToList();
                entriesToProcess = entries.ToList();
            }
            var count = entriesToProcess.Count;

            for (var i = 0; i < count - 1; i++)
            {
                toggleService.Add(
                    entriesToProcess[i].FileName,
                    entriesToProcess[i].SwitchToTime,
                    entriesToProcess[i + 1].SwitchToTime,
                    entriesToProcess[i].Category == "Good");
            }
            toggleService.Add(entriesToProcess[count].FileName,
                    entriesToProcess[count].SwitchToTime,
                    time,
                    entriesToProcess[count].Category == "Good");

        }

        #endregion

        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
