using System;
using System.Windows;
using System.Windows.Forms;
using MooBoo.ViewModel;

namespace MooBoo.View
{
    /// <summary>
    /// Interaction logic for MooBooTrayApp.xaml
    /// </summary>
    public partial class MooBooTrayApp : Window
    {
        #region Fileds

        readonly MooBooTrayAppViewModel dataContext = new MooBooTrayAppViewModel();

        #endregion

        #region Constructors

        public MooBooTrayApp()
        {
            InitializeComponent();
            InitializeDataContext();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            this.Hide();
            NotifyIcon ni = new NotifyIcon();
            ni.Icon = dataContext.TrayIcon;
            ni.Visible = true;
            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
        }

        private void InitializeDataContext()
        {
            DataContext = dataContext;
        }

        #endregion
    }
}
