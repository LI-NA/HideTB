namespace HideTB.Utils
{
    internal class StartupManager
    {
        public static void Config(bool enable)
        {
            try
            {
                const string keyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                using var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyName, true);
                if (key != null)
                {
                    if (enable)
                    {
                        key.SetValue("HideTB", Environment.ProcessPath ?? string.Empty);
                    }
                    else
                    {
                        key.DeleteValue("HideTB", false);
                    }
                }
            }
            catch
            {
                // Requires appropriate permissions or handle errors
            }
        }
    }
}
