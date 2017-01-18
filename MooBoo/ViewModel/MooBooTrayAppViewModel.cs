using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using MooBoo.ActiveWindowHook;
using MooBoo.Annotations;
using MooBoo.Resources;
using MooBoo.Utility;
using System.Windows;
using System.Configuration;

namespace MooBoo.ViewModel
{
    public class MooBooTrayAppViewModel : INotifyPropertyChanged
    {
        #region Fileds

        private readonly ActiveWindowHooker windowHooker = new ActiveWindowHooker();
        #endregion

        #region Constructor

        public MooBooTrayAppViewModel()
        {
            windowHooker.ActiveWindowChanged += OnActiveWindowChanged;
            windowHooker.Init();
        }

        #endregion

        #region Properties

        public Icon TrayIcon
        {
            get { return Icons.TrayIcon; }
        }

        
        public string ApiTokenProperty
        {
            get
            {
                try
                {
                    var apiToken = Properties.Settings.Default["ApiToken"];
                    
                    return apiToken.ToString();
                } catch(Exception e)
                { }
                return string.Empty;
            }

            set
            {
                Properties.Settings.Default["ApiToken"] = value;
                Properties.Settings.Default.Save();
            }
        }

        #endregion

        #region Methods

        private void OnActiveWindowChanged(object sender, ActiveWindowChangedEventArgs e)
        {
            //TODO dinner is served bitchers
            //Scheduler.Instance.Schedule("pidor", TimeSpan.FromSeconds(6), () =>
            //{
            //    Console.WriteLine(DateTime.Now);
            //});
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
