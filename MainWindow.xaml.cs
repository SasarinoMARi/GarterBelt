using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
			//ConsoleManager.Show();
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

		private void SetProcess(GarterProcesses g)
		{
			this.pId = g.MainWindowHandle;
			this.labelHandleId.Content = "PNAME : " + g.Name;
			var opacity = WindowAnnotation.SetWindowToStyled(pId);
			this.sliderOpacity.Value = opacity;
			if (!this.panelMainControls.IsEnabled) this.panelMainControls.IsEnabled = true;
		}

		private void FindHandle(string name)
		{
			Process[] processRunning = Process.GetProcesses();
			foreach (Process p in processRunning)
			{
				if (p.MainWindowHandle.ToInt32() == 0) continue;
				if (p.ProcessName.ToLower().Contains(name))
				{
					Garterbelts garters = Resources["garters"] as Garterbelts;
					if (garters.Any(x => x.ProcessId == p.Id && x.Name == p.ProcessName)) continue;
					else
					{
						var g = new GarterProcesses(p);
						garters.Add(g);
						SetProcess(g);
					}
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
			var lines = string.Empty;
			Garterbelts garters = Resources["garters"] as Garterbelts;
			foreach (var item in garters)
			{
				lines += item.ToString() + '\n';
			}
			File.WriteAllText("id.txt", lines);
		}

		private void LoadHandle()
		{
			if (!File.Exists("id.txt")) return;
			Garterbelts garters = Resources["garters"] as Garterbelts;
			var temp = File.ReadAllText("id.txt");
			if (temp == null) return;
			var lines = temp.Split('\n');
			foreach (var line in lines)
			{
				var item = GarterProcesses.LoadFromLine(line);
				if (item == null) continue;
				if (garters.Any(x => x.ProcessId == item.ProcessId && x.Name == item.Name)) continue;
				garters.Add(item);
			}

			if (garters.Count > 0) SetProcess(garters.First());
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

		private void button_FindProcessDialogOK_Click(object sender, RoutedEventArgs e)
		{
			var pName = this.textBoxProcessName.Text;
			if (string.IsNullOrWhiteSpace(pName)) return;
			FindHandle(pName);
		}

		private void button_SelectDialog_Click(object sender, RoutedEventArgs e)
		{
			if (sender == this.buttonSDOk)
			{
				var g = this.listView.SelectedItem as GarterProcesses;
				if (g == null) return;
				SetProcess(g);
			}
			if (sender == this.buttonSDRemove)
			{
				Garterbelts garters = Resources["garters"] as Garterbelts;
				garters.Remove(this.listView.SelectedItem as GarterProcesses);
			}
		}

		private void textBoxProcessName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				typeof(System.Windows.Controls.Primitives.ButtonBase).GetMethod("OnClick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(buttonDialogOK, new object[0]);
			}
		}
	}
}
