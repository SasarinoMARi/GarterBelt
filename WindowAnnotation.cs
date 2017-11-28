using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GarterBelt
{
	class WindowAnnotation
	{
		public enum WindowState { SW_HIDE = 0, SW_SHOWNORMAL = 1, SW_NORMAL = 1, SW_SHOWMINIMIZED = 2, SW_SHOWMAXIMIZED = 3, SW_MAXIMIZED = 3, SW_SHOWNOACTIVE = 4, SW_SHOW = 5, SW_MINIMIZED = 6, SW_SHOWMININOACTIVE = 7, SW_SHOWNA = 8, SW_RESTORE = 9, SW_SHOWDEFAULT = 10, SW_FORCEMINIMIZED = 11, SW_MAX = 11 }

		[DllImport("User32")]
		public static extern int ShowWindow(int hwnd, WindowState nCmdShow);
	}
}
