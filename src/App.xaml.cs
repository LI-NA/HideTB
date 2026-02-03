using H.NotifyIcon;
using HideTB.Properties;
using HideTB.Utils;
using System.Windows;

namespace HideTB
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon? _notifyIcon;
        private readonly TaskbarWatcher _taskbarWatcher = new();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
            if (_notifyIcon != null)
            {
                _notifyIcon.ForceCreate();
            }
            else
            {
                MessageBox.Show(HideTB.Properties.Resources.Message_TrayError, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
                return;
            }

            StartupManager.Config(Settings.Default.Startup);

            HideTaskbar.Hide();
            _taskbarWatcher.StartWatching();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _notifyIcon?.Dispose();
            _taskbarWatcher.StopWatching();
            HideTaskbar.Restore();
            Settings.Default.Save();
            base.OnExit(e);
        }
    }
}
