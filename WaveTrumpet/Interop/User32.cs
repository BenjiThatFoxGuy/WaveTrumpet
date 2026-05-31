using System;
using System.Runtime.InteropServices;

namespace WaveTrumpet.Interop
{
    public class User32
    {
        [Flags]
        public enum WindowPosFlags : uint
        {
            SWP_NOSIZE = 0x0001,
            SWP_NOMOVE = 0x0002,
            SWP_NOZORDER = 0x0004,
            SWP_NOACTIVATE = 0x0010,
        }

        [DllImport("user32.dll", PreserveSig = true)]
        public static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int x,
            int y,
            int cx,
            int cy,
            WindowPosFlags uFlags);

        [DllImport("user32.dll", PreserveSig = true)]
        internal static extern int SetWindowCompositionAttribute(
            IntPtr hwnd,
            ref WindowCompositionAttribData data);

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttribData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public AccentFlags AccentFlags;
            public uint GradientColor;
            public uint AnimationId;
        }

        [Flags]
        public enum AccentFlags
        {
            None = 0x0,
            DrawLeftBorder = 0x20,
            DrawTopBorder = 0x40,
            DrawRightBorder = 0x80,
            DrawBottomBorder = 0x100,
            DrawAllBorders = DrawLeftBorder | DrawTopBorder | DrawRightBorder | DrawBottomBorder,
        }

        internal enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19,
            WCA_CORNER_STYLE = 27,
        }

        internal enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
            ACCENT_INVALID_STATE = 5,
        }

        public const int WS_EX_TOOLWINDOW = 0x00000080;

        public enum GWL : int
        {
            GWL_STYLE = -16,
            GWL_EXSTYLE = -20,
        }

#if X86
        [DllImport("user32.dll", CharSet = CharSet.Unicode, PreserveSig = true)]
        public static extern int GetWindowLong(
            IntPtr hWnd,
            GWL nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, PreserveSig = true)]
        public static extern int SetWindowLong(
            IntPtr hWnd,
            GWL nIndex,
            int dwNewLong);
#else
#error [Get/Set]WindowLong not supported on 64-bit platforms
#endif
    }
}
