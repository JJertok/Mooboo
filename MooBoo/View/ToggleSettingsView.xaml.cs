using System.ComponentModel;

namespace MooBoo.View
{
    /// <summary>
    /// Interaction logic for ToggleSettingsView.xaml
    /// </summary>
    public partial class ToggleSettingsView
    {
        public ToggleSettingsView()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
