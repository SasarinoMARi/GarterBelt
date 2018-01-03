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

		[DllImport("user32.dll")]
		static extern bool SetLayeredWindowAttributes(int hwnd, uint crKey, byte bAlpha, uint dwFlags);
		[DllImport("user32.dll")]
		static extern bool GetLayeredWindowAttributes(int hwnd, out uint crKey, out byte bAlpha, out uint dwFlags);

		[DllImport("user32")]
		public static extern Int32 GetWindowLong(int hWnd, Int32 nIndex);

		[DllImport("user32")]
		public static extern Int32 SetWindowLong(int hWnd, Int32 nIndex, Int32 dwNewLong);

		public const int GWL_EXSTYLE = -20;
		public const int WS_EX_LAYERED = 0x80000;
		public const int LWA_ALPHA = 0x2;
		public const int LWA_COLORKEY = 0x1;

		[DllImport("user32.dll")]
		static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		private const int HWND_TOPMOST = -1;
		private const int HWND_NOTOPMOST = -2;
		private const int SWP_NOMOVE = 0x0002;
		private const int SWP_NOSIZE = 0x0001;

		public static int SetWindowToStyled(int hwnd)
		{
			var cur = GetWindowLong(hwnd, GWL_EXSTYLE);
			byte opacity = 0;
			if (cur == 256)
			{
				Console.WriteLine("EX Window style applied.");
				SetWindowLong(hwnd, GWL_EXSTYLE, cur ^ WS_EX_LAYERED);
				opacity = 255;
			}
			else {
				opacity = GetOpacity(hwnd);
			}
			Console.WriteLine("window opacity : " + opacity);
			return opacity;
		}
		public static byte GetOpacity(int hwnd)
		{
			uint crKey, dwFlags;
			byte bAlpha;
			GetLayeredWindowAttributes(hwnd, out crKey, out bAlpha, out dwFlags);
			return bAlpha;
		}

		public static bool SetOpacity(int hwnd, byte opacity)
		{
			return SetLayeredWindowAttributes(hwnd, 0, opacity, LWA_ALPHA);
		}

		public static void SetTopmost(int hwnd, bool topmost)
		{
			SetWindowPos(hwnd, (topmost) ? HWND_TOPMOST : HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
		}

	}
}
