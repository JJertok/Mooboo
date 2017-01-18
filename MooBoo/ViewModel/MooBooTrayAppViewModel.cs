using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using MooBoo.ActiveWindowHook;
using MooBoo.Annotations;
using MooBoo.Resources;

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

        #endregion

        #region Methods

        private void OnActiveWindowChanged(object sender, ActiveWindowChangedEventArgs e)
        {
            //TODO dinner is served bitchers
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
