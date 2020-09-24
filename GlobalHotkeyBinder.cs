using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GarterBelt
{
	class GlobalHotkeyBinder
	{
		List<KeyboardHook> hookers = new List<KeyboardHook>();
		public GlobalHotkeyBinder() {

		}

        public void Clear() {
            foreach (var hook in hookers) {
                hook.UnregisterHotKey();
            }
            hookers.Clear();
        }

		public void AddBindHandler(uint key, uint modifiers, Action<object, KeyPressedEventArgs> callback)
		{
			var hook = new KeyboardHook(modifiers, key);
			hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(callback);
			hook.RegisterHotKey();
			hookers.Add(hook);
		}

		private sealed class KeyboardHook : IDisposable
		{
			[DllImport("user32.dll")]
			private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
			[DllImport("user32.dll")]
			private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

			private class Window : NativeWindow, IDisposable
			{
				private static int WM_HOTKEY = 0x0312;

				public Window()
				{
					this.CreateHandle(new CreateParams());
				}

				protected override void WndProc(ref Message m)
				{
					base.WndProc(ref m);
					
					if (m.Msg == WM_HOTKEY)
					{
						Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
						ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

						if (KeyPressed != null)
							KeyPressed(this, new KeyPressedEventArgs(modifier, key));
					}
				}

				public event EventHandler<KeyPressedEventArgs> KeyPressed;

				#region IDisposable Members

				public void Dispose()
				{
					this.DestroyHandle();
				}

				#endregion
			}

			private Window _window = new Window();
            private readonly uint modifier;
            private readonly uint key;

            public KeyboardHook(uint modifier, uint key)
			{
				_window.KeyPressed += delegate (object sender, KeyPressedEventArgs args)
				{
					if (KeyPressed != null)
						KeyPressed(this, args);
				};
                this.modifier = modifier;
                this.key = key;
            }

			public void RegisterHotKey()
			{
				if (!RegisterHotKey(_window.Handle, 0, (uint)modifier, (uint)key))
					throw new InvalidOperationException("Couldn’t register the hot key.");
			}

            public void UnregisterHotKey() {
                if (!UnregisterHotKey(_window.Handle, 0))
                    throw new InvalidOperationException("Couldn’t register the hot key.");
            }

			public event EventHandler<KeyPressedEventArgs> KeyPressed;

			#region IDisposable Members

			public void Dispose()
			{
                UnregisterHotKey();
                _window.Dispose();
			}

			#endregion
		}
	}

	public class KeyPressedEventArgs : EventArgs
	{
		private ModifierKeys _modifier;
		private Keys _key;

		internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
		{
			_modifier = modifier;
			_key = key;
		}

		public ModifierKeys Modifier
		{
			get { return _modifier; }
		}

		public Keys Key
		{
			get { return _key; }
		}
	}

	[Flags]
	public enum ModifierKeys : uint
	{
        None        = 0x0,
		Alt         = 0x1 << 0,
		Control     = 0x1 << 1,
		Shift       = 0x1 << 2,
		Win         = 0x1 << 3
    }

}

