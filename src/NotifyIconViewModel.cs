using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HideTB.Properties;
using HideTB.Utils;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace HideTB;

/// <summary>
/// Provides bindable properties and commands for the NotifyIcon.
/// </summary>
public partial class NotifyIconViewModel : ObservableObject
{
    [RelayCommand]
    public void SaveSettings()
    {
        Settings.Default.Save();
    }

    [RelayCommand]
    public void ToggleStartup()
    {
        StartupManager.Config(Settings.Default.Startup);
        Settings.Default.Save();
    }

    [RelayCommand]
    public void ShowGithub()
    {
        string url = "https://github.com/LI-NA/HideTB";
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch
        {
            // Ignore errors
        }
    }

    [RelayCommand]
    public void ExitApplication()
    {
        Application.Current.Shutdown();
    }

    public string ApplicationTitle
    {
        get
        {
            var assembly = Assembly.GetEntryAssembly();
            var name = assembly?.GetName().Name;
            var version = assembly?.GetName().Version;
            return $"{name} v{version?.Major}.{version?.Minor}.{version?.Build}";
        }
    }
}