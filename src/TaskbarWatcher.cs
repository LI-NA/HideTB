using HideTB.Properties;
using HideTB.Utils;
using System.Text;

namespace HideTB
{
    internal class TaskbarWatcher
    {
        private CancellationTokenSource? _cts;
        private IntPtr _taskbarHandle = IntPtr.Zero;
        private DateTime? _hideRequestTime;
        private bool _isVisible;

        public async void StartWatching()
        {
            if (_cts != null) return;

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            UpdateTaskbarHandle();

            try
            {
                await Task.Run(async () =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        ProcessTaskbarState();
                        await Task.Delay(Settings.Default.Tick, token);
                    }
                }, token);
            }
            catch (TaskCanceledException)
            {
                // Task was cancelled, expected behavior on stop
            }
        }

        public void StopWatching()
        {
            _cts?.Cancel();
            _cts = null;
        }

        private void ProcessTaskbarState()
        {
            if (!Settings.Default.Enabled)
            {
                if (!_isVisible)
                {
                    _isVisible = true;
                    HideTaskbar.Restore();
                }
                return;
            }

            bool mouseInTaskbar = IsMouseInTaskbar();
            bool shouldBeVisible = mouseInTaskbar || IsMenuVisible();

            if (shouldBeVisible)
            {
                _hideRequestTime = null;

                if (!_isVisible)
                {
                    _isVisible = true;
                    HideTaskbar.Restore();
                }
            }
            else
            {
                if (_isVisible)
                {
                    if (_hideRequestTime == null)
                    {
                        _hideRequestTime = DateTime.Now;
                    }

                    if ((DateTime.Now - _hideRequestTime.Value).TotalMilliseconds > Settings.Default.Delay)
                    {
                        _isVisible = false;
                        _hideRequestTime = null;
                        HideTaskbar.Hide();
                    }
                }
            }
        }

        private void UpdateTaskbarHandle()
        {
            if (_taskbarHandle == IntPtr.Zero || !NativeMethods.IsWindow(_taskbarHandle))
            {
                _taskbarHandle = NativeMethods.FindWindow("Shell_TrayWnd", null);
            }
        }

        private bool IsMouseInTaskbar()
        {
            if (_taskbarHandle == IntPtr.Zero)
            {
                UpdateTaskbarHandle();
                if (_taskbarHandle == IntPtr.Zero) return false;
            }

            if (!NativeMethods.GetWindowRect(_taskbarHandle, out var rect))
            {
                _taskbarHandle = IntPtr.Zero;
                return false;
            }

            NativeMethods.GetCursorPos(out var mousePos);

            return mousePos.X >= rect.Left && mousePos.X <= rect.Right &&
                   mousePos.Y >= rect.Top && mousePos.Y <= rect.Bottom;
        }

        private bool IsMenuVisible()
        {
            IntPtr foregroundWindow = NativeMethods.GetForegroundWindow();
            if (foregroundWindow == IntPtr.Zero) return false;

            StringBuilder className = new StringBuilder(256);
            NativeMethods.GetClassName(foregroundWindow, className, className.Capacity);
            string currentClass = className.ToString();

            // Check for specific system windows that should keep the taskbar visible
            if (currentClass is "NotifyIconOverflowWindow"
                             or "TopLevelWindowForOverflowXamlIsland"
                             or "ControlCenterWindow"
                             or "Windows.UI.Core.CoreWindow")
            {
                return true;
            }

            if (NativeMethods.IsWindowVisible(NativeMethods.FindWindow("NotifyIconOverflowWindow", null))) return true;
            if (NativeMethods.IsWindowVisible(NativeMethods.FindWindow("TopLevelWindowForOverflowXamlIsland", null))) return true;

            return false;
        }
    }
}
