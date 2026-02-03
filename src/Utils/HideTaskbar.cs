namespace HideTB.Utils
{
    public class HideTaskbar
    {
        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_LAYERED = 0x80000;
        private const int LWA_ALPHA = 0x2;

        public static void Hide()
        {
            IntPtr hwnd = NativeMethods.FindWindow("Shell_TrayWnd", null);
            if (hwnd != IntPtr.Zero)
            {
                int style = NativeMethods.GetWindowLong(hwnd, GWL_EXSTYLE);
                NativeMethods.SetWindowLong(hwnd, GWL_EXSTYLE, style | WS_EX_LAYERED);

                NativeMethods.SetLayeredWindowAttributes(hwnd, 0, 0, LWA_ALPHA);
            }
        }

        public static void Restore()
        {
            IntPtr hwnd = NativeMethods.FindWindow("Shell_TrayWnd", null);
            if (hwnd != IntPtr.Zero)
            {
                NativeMethods.SetLayeredWindowAttributes(hwnd, 0, 255, LWA_ALPHA);

                int style = NativeMethods.GetWindowLong(hwnd, GWL_EXSTYLE);
                NativeMethods.SetWindowLong(hwnd, GWL_EXSTYLE, style & ~WS_EX_LAYERED);
            }
        }
    }
}
