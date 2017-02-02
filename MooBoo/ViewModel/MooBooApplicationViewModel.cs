using System;
using System.Linq;
using System.Windows.Forms;
using MooBoo.ActiveWindowHook;
using MooBoo.DataAcessLayer.SqliteDataProvider;
using MooBoo.Model;
using MooBoo.Model.DataLayer;
using ToggleSandbox.Services;
using Application = MooBoo.Model.DataLayer.Application;

namespace MooBoo.ViewModel
{
    public class MooBooApplicationViewModel : ViewModelBase
    {
        #region Fields

        private readonly ToggleModuleSettingsViewModel _settingsViewModel;
        private readonly ActiveWindowHooker _windowHooker;
        private WindowChangeEntry _previousEntry;
        private LogService _logService;

        public MooBooApplicationViewModel() : base(null)
        {
            _settingsViewModel = new ToggleModuleSettingsViewModel(this);
            InitTrayIcon();
            _windowHooker = new ActiveWindowHooker();
            _windowHooker.ActiveWindowChanged += OnActiveWindowChanged;
            _windowHooker.Init();
            InitLogService();
        }

        #endregion

        #region Methods

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
            var app = SqliteApplicationProvider.Instance.ReadAll().FirstOrDefault(a => a.FileName == e.ProcessFileName);
            if (app == null)
            {
                app = SqliteApplicationProvider.Instance.Create(new Application
                {
                    FileName = e.ProcessFileName,
                });
            }
            var current = new WindowChangeEntry(app.Id, "Category", DateTime.Now);
            if (_previousEntry != null)
            {
                SqliteDataProvider.Instance.Create(new LogItem
                {
                    ApplicationId = _previousEntry.ApplicationId,
                    Start = _previousEntry.SwitchToTime,
                    Stop = current.SwitchToTime
                });
            }

            _previousEntry = current;

            InitLogService();
            _logService.Add(SqliteApplicationProvider.Instance.Read(_previousEntry.ApplicationId).FileName, _previousEntry.SwitchToTime, current.SwitchToTime, true);
        }

        private void InitLogService()
        {
            if (string.IsNullOrEmpty(_settingsViewModel.ApiToken) ||
                    (_logService != null &&
                    _logService.GetToken() == _settingsViewModel.ApiToken)
               )
            {
                return;
            }
            _logService = new LogService(_settingsViewModel.ApiToken);
        }

        #endregion

        #region Events

        public event EventHandler Exit;

        #endregion
    }
}
