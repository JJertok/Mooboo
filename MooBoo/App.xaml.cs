using System;
using System.Threading;
using System.Windows;
using MooBoo.ViewModel;

namespace MooBoo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App 
    {
        

        /// <summary>
        /// Application entry point
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

            var app = new MooBooApplicationViewModel();
            app.Exit += OnAppExit;
            
        }

        /// <summary>
        /// Application exit point
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnAppExit(object sender, EventArgs e)
        {
            Current.Shutdown();
        }
    }

}
