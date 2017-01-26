using System;
using System.Windows.Forms;
using MooBoo.ActiveWindowHook;
using MooBoo.DataAcessLayer;
using MooBoo.Model;
using MooBoo.Model.DataLayer;

namespace MooBoo.ViewModel
{
    public class MooBooApplicationViewModel : ViewModelBase
    {
        #region Fields

        private readonly ToggleModuleSettingsViewModel _settingsViewModel;
        private readonly ActiveWindowHooker _windowHooker;
        private WindowChangeEntry _previousEntry;

        public MooBooApplicationViewModel() : base(null)
        {
            this._settingsViewModel = new ToggleModuleSettingsViewModel(this);
            _windowHooker = new ActiveWindowHooker();
            _windowHooker.ActiveWindowChanged += OnActiveWindowChanged;
            _windowHooker.Init();
        }

        #endregion

        #region Methods

        public void Init()
        {
            InitTrayIcon();
        }

        private NotifyIcon InitTrayIcon()
        {
            return new NotifyIcon
            {
                Icon = Resources.Icons.TrayIcon,
                Visible = true,
                ContextMenu = InitTrayMenu()
            };
        }

        private ContextMenu InitTrayMenu()
        {
            var result = new ContextMenu();

            var settingsButton = new MenuItem(Resources.UiStrings.TrayMenuSettingsLabel);
            settingsButton.Click += _settingsViewModel.OpenView;

            var exitButton = new MenuItem(Resources.UiStrings.TrayMenuExitLabel);
            exitButton.Click += OnExitButtonClicked;

            result.MenuItems.Add(settingsButton);
            result.MenuItems.Add(exitButton);

            return result;
        }

        private void OnExitButtonClicked(object sender, EventArgs e)
        {
            var handler = Exit;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void OnActiveWindowChanged(object sender, ActiveWindowChangedEventArgs e)
        {
            if (e.ProcessFileName == string.Empty)
            {
                return;
            }
            var current = new WindowChangeEntry(e.ProcessFileName, "Category", DateTime.Now);
            if (_previousEntry != null)
            {
                DataProvider.Instance.Create(new LogItem
                {
                    FileName = _previousEntry.FileName,
                    Start = _previousEntry.SwitchToTime,
                    Stop = current.SwitchToTime
                });
            }

            _previousEntry = current;
        }

        #endregion

        #region Events

        public event EventHandler Exit;

        #endregion
    }
}
