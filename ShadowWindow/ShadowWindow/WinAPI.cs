using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace ShadowWindow
{
    public struct Margins
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    public class WinAPI
    {
        /// <summary>
        /// https://docs.microsoft.com/en-us/windows/win32/api/dwmapi/ne-dwmapi-dwmwindowattribute
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="attr"></param>
        /// <param name="attrValue"></param>
        /// <param name="attrSize"></param>
        /// <returns></returns>
        [DllImport("dwmapi.dll", PreserveSig = true)]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="pMarInset"></param>
        /// <returns></returns>

        [DllImport("dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMarInset);


        private const int DWMWA_NCRENDERING_ENABLED = 1;           // [get] Is non-client rendering enabled/disabled
        private const int DWMWA_NCRENDERING_POLICY = 2;            // [set] Non-client rendering policy
        private const int DWMWA_TRANSITIONS_FORCEDISABLED = 3;     // [set] Potentially enable/forcibly disable transitions
        private const int DWMWA_ALLOW_NCPAINT = 4;                 // [set] Allow contents rendered in the non-client area to be visible on the DWM-drawn frame.
        private const int DWMWA_CAPTION_BUTTON_BOUNDS= 5;          // [get] Bounds of the caption button area in window-relative space.
        private const int DWMWA_NONCLIENT_RTL_LAYOUT = 6;          // [set] Is non-client content RTL mirrored
        private const int DWMWA_FORCE_ICONIC_REPRESENTATION = 7;   // [set] Force this window to display iconic thumbnails.
        private const int DWMWA_FLIP3D_POLICY = 8;                 // [set] Designates how Flip3D will treat the window.
        private const int DWMWA_EXTENDED_FRAME_BOUNDS = 9;         // [get] Gets the extended frame bounds rectangle in screen space
        private const int DWMWA_HAS_ICONIC_BITMAP = 10;            // [set] Indicates an available bitmap when there is no better thumbnail representation.
        private const int DWMWA_DISALLOW_PEEK = 11;                // [set] Don't invoke Peek on the window.
        private const int DWMWA_EXCLUDED_FROM_PEEK = 12;           // [set] LivePreview exclusion information
        private const int DWMWA_CLOAK = 13;                        // [set] Cloak or uncloak the window
        private const int DWMWA_CLOAKED = 14;                      // [get] Gets the cloaked state of the window
        private const int DWMWA_FREEZE_REPRESENTATION = 15;        // [set] Force this window to freeze the thumbnail without live update
        private const int DWMWA_LAST = 16;

        private const int S_OK = 0;

        public static bool DropShadow(Window window,Margins margin)
        {
            try
            {
                WindowInteropHelper helper = new WindowInteropHelper(window);
                int val = DWMWA_NCRENDERING_POLICY;
                int ret1 = DwmSetWindowAttribute(helper.Handle, DWMWA_NCRENDERING_POLICY, ref val, 4);

                if (ret1 == S_OK)
                {
                    int ret2 = DwmExtendFrameIntoClientArea(helper.Handle, ref margin);
                    return ret2 == S_OK;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
