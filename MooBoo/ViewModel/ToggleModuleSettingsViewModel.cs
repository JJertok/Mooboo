using System;
using System.Windows.Input;
using MooBoo.UiSpecific;
using MooBoo.View;

namespace MooBoo.ViewModel
{
    public class ToggleModuleSettingsViewModel : ViewModelBase
    {
        #region Fields

        private ToggleSettingsView _window;

        #endregion

        #region Methods

        public void OpenView(object sender, EventArgs eventArgs)
        {
            if (_window == null)
            {
                _window = new ToggleSettingsView()
                {
                    ShowInTaskbar = false,
                    ShowActivated = false
                };
            }

            _window.DataContext = this;
            _window.Show();
        }

        #endregion

        #region Properties

        public string ApiTokenProperty
        {
            get
            {
                try
                {
                    var apiToken = Properties.Settings.Default["ApiToken"];

                    return apiToken.ToString();
                }
                catch (Exception e)
                {
                    //TODO WTF?
                }
                return string.Empty;
            }

            set
            {
                Properties.Settings.Default["ApiToken"] = value;
                Properties.Settings.Default.Save();
            }
        }

        #endregion

        #region Commands

        public ICommand OkCommand { get; set; }

        private void OnOkCommand(object obj)
        {
            _window.Hide();
        }
        private bool CanOkCommand(object obj)
        {
            return true;
        }

        #endregion

        public ToggleModuleSettingsViewModel(ViewModelBase parent) : base(parent)
        {
             OkCommand = new RelayCommand(CanOkCommand, OnOkCommand);
        }

        
    }
}
