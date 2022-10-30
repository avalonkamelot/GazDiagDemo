using System;
using System.Windows;

namespace TubeDemo.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            DispatcherUnhandledException += Application_DispatcherUnhandledException;
        }

        ~App()
        {
            DispatcherUnhandledException -= Application_DispatcherUnhandledException;
        }

        [STAThread]
        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += Thread_UnhandledException;
            new MainWindow().Show();
        }


     

        #region Exception Handling
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "UnhandledException");
            e.Handled = true;
        }

        private void Thread_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception).Message, "Thread_UnhandledException");
            // there is no way to restore session, see error for details
            Current.Shutdown(1);
        }
        #endregion
    }
}
