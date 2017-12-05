using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GarterBelt
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		int pId = 0;
		GlobalHotkeyBinder binder = new GlobalHotkeyBinder();

		public MainWindow()
		{
			ConsoleManager.Show();
			InitializeComponent();
		
			binder.AddBindHandler("Handler1", Keys.F1, ModifierKeys.Shift, delegate (object sender, KeyPressedEventArgs e)
			{
				HideByHandle();
			});
			binder.AddBindHandler("Handler2", Keys.F2, ModifierKeys.Shift, delegate (object sender, KeyPressedEventArgs e)
			{
				ShowByHandle();
			});

			LoadHandle();
		}

		private void FindProcessID(int id)
		{
			this.pId = id;
			this.labelHandleId.Content = "PID : " + id;
			var opacity = WindowAnnotation.SetWindowToStyled(pId);
			this.sliderOpacity.Value = opacity;
		}

		private void FindHandle(string name)
		{
			Process[] processRunning = Process.GetProcesses();
			foreach (Process p in processRunning)
			{
				if (p.ProcessName.ToLower().Contains(name))
				{
					FindProcessID(p.MainWindowHandle.ToInt32());
				}
			}
		}

		private void ShowByHandle()
		{
			if (pId == 0) return;
			WindowAnnotation.ShowWindow(pId, WindowAnnotation.WindowState.SW_SHOW);
		}

		private void HideByHandle()
		{
			if (pId == 0) return;
			WindowAnnotation.ShowWindow(pId, WindowAnnotation.WindowState.SW_HIDE);
		}

		private void ExportHandle()
		{
			File.WriteAllText("id.txt", pId.ToString());
		}

		private void LoadHandle()
		{
			var temp = File.ReadAllText("id.txt");
			if (temp == null) return;

			int id = 0;
			int.TryParse(temp, out id);
			if (id == 0) return;

			FindProcessID(id);
		}

		private void OpacityByHandle(byte opacity)
		{
			WindowAnnotation.SetOpacity(pId, opacity);
		}

		private void NoTopmostByHandle()
		{
			WindowAnnotation.SetTopmost(pId, false);
		}

		private void TopmostByHandle()
		{
			WindowAnnotation.SetTopmost(pId, true);
		}


		private void button_Click(object sender, RoutedEventArgs e)
		{
			//if (sender == this.buttonFindHandle) DialogHost.Show(this.dialogFindWindow);
			//FindHandle("ffxiv");

			if (sender == this.buttonLoadHandle) LoadHandle();
			if (sender == this.buttonExportHandle) ExportHandle();

			if (sender == this.buttonHideByHandle) HideByHandle();
			if (sender == this.buttonShowByHandle) ShowByHandle();

			if (sender == this.buttonTopmost) TopmostByHandle();
			if (sender == this.buttonNoTopmost) NoTopmostByHandle();
		}

		private void sliderOpacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			OpacityByHandle(byte.Parse(((int)this.sliderOpacity.Value).ToString()));
		}

		private void buttonDialogOK_Click(object sender, RoutedEventArgs e)
		{
			var pName = this.textBoxProcessName.Text;
			if (string.IsNullOrWhiteSpace(pName)) return;
			FindHandle(pName);
		}
	}
}
