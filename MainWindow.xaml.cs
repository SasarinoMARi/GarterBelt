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
			ConsoleManager.Init();
#if !DEBUG
			ConsoleManager.Hide();
#endif
			InitializeComponent();

			#region key binding
			binder.AddBindHandler("windowHideHandler", Keys.F1, ModifierKeys.Shift, delegate (object sender, KeyPressedEventArgs e)
			{
				HideByHandle();
			});
			binder.AddBindHandler("windowShowHandler", Keys.F2, ModifierKeys.Shift, delegate (object sender, KeyPressedEventArgs e)
			{
				ShowByHandle();
			});
			binder.AddBindHandler("garterHideHandler", Keys.F1, ModifierKeys.Control, delegate (object sender, KeyPressedEventArgs e)
			{
				HideGarter();
			});
			binder.AddBindHandler("garterShowHandler", Keys.F2, ModifierKeys.Control, delegate (object sender, KeyPressedEventArgs e)
			{
				ShowGarter();
			});
			binder.AddBindHandler("garterToggleConsoleHandler", Keys.F3, ModifierKeys.Control, delegate (object sender, KeyPressedEventArgs e)
			{
				ToggleConsole();
			});
			#endregion

			LoadHandle();
		}

		private void ShowGarter()
		{
			this.Show();
		}
		private void HideGarter()
		{
			this.Hide();
		}
		private void ToggleConsole()
		{
			ConsoleManager.Toggle();
		}

		private void SetProcess(GarterProcesses g)
		{
			if (g == null)
			{
				Console.WriteLine("선택된 프로세스가 없습니다.");
				this.pId = 0;
				this.labelHandleId.Content = "선택된 프로세스가 없습니다.";
				this.sliderOpacity.Value = this.sliderOpacity.Maximum;
				this.panelMainControls.IsEnabled = false;
				ExportHandle();
			}
			else
			{
				Console.WriteLine(string.Format("프로세스 {0} 대상으로 garterbelt를 초기화합니다.", g.Name));
				this.pId = g.MainWindowHandle;
				this.labelHandleId.Content = "활성화된 프로세스 : " + g.Name;
				var opacity = WindowAnnotation.SetWindowToStyled(pId);
				this.sliderOpacity.Value = opacity;
				if (!this.panelMainControls.IsEnabled) this.panelMainControls.IsEnabled = true;
				ExportHandle();
			}
		}
		private void FindHandle(string name)
		{
			name = name.ToLower();
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

		private void ExportHandle()
		{
			var lines = string.Empty;
			Garterbelts garters = Resources["garters"] as Garterbelts;
			lines += pId.ToString() + '\n';
			foreach (var item in garters)
			{
				lines += item.ToString() + '\n';
			}
			File.WriteAllText("id.txt", lines);
			Console.WriteLine("solution export finished.");
		}
		private void LoadHandle()
		{
			if (!File.Exists("id.txt")) return;
			Garterbelts garters = Resources["garters"] as Garterbelts;
			var temp = File.ReadAllText("id.txt");
			if (temp == null) return;
			var lines = temp.Split('\n');
			int savedHandle = 0; int.TryParse(lines[0], out savedHandle);
			foreach (var line in lines)
			{
				var item = GarterProcesses.LoadFromLine(line);
				if (item == null) continue;
				if (!ValidateHandle(item.ProcessId)) continue;
				if (garters.Any(x => x.ProcessId == item.ProcessId && x.Name == item.Name)) continue;
				garters.Add(item);
			}
			Console.WriteLine("solution import finished.");
			Console.WriteLine(string.Format("파일에서 불러온 프로세스 수 : {0}", garters.Count.ToString()));
			Console.WriteLine(string.Format("최근 활성화된 프로세스 핸들 : {0}", savedHandle.ToString()));
			if (garters.Count > 0)
			{
				var query = garters.Where(item => item.MainWindowHandle == savedHandle);
				if (query.Count() > 0) SetProcess(query.First());
				else SetProcess(garters.First());
			}
		}
		private bool ValidateHandle(int processId)
		{
			bool isValidate = true;
			try
			{
				var p = Process.GetProcessById(processId);
			}
			catch
			{
				isValidate = false;
			}
			return isValidate;
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
		private void DisableTopmostByHandle()
		{
			WindowAnnotation.SetTopmost(pId, false);
		}
		private void TopmostByHandle()
		{
			WindowAnnotation.SetTopmost(pId, true);
		}
		private void OpacityByHandle(byte opacity)
		{
			WindowAnnotation.SetOpacity(pId, opacity);
		}

		#region UI Interactions

		private void Button_Click_Common(object sender, RoutedEventArgs e)
		{
			if (sender == this.buttonHideByHandle) HideByHandle();
			if (sender == this.buttonShowByHandle) ShowByHandle();

			if (sender == this.buttonTopmost) TopmostByHandle();
			if (sender == this.buttonNoTopmost) DisableTopmostByHandle();
		}
		private void SliderOpacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			OpacityByHandle(byte.Parse(((int)this.sliderOpacity.Value).ToString()));
		}
		private void Button_FindProcessDialogOK_Click(object sender, RoutedEventArgs e)
		{
			var pName = this.textBoxProcessName.Text;
			if (string.IsNullOrWhiteSpace(pName)) return;
			FindHandle(pName);
			this.textBoxProcessName.Text = string.Empty;
		}
		private void Button_SelectDialog_Click(object sender, RoutedEventArgs e)
		{
			if (sender == this.buttonSDOk)
			{
				var g = this.listview_garters.SelectedItem as GarterProcesses;
				if (g == null) return;
				SetProcess(g);
			}
			if (sender == this.buttonSDRemove)
			{
				Garterbelts garters = Resources["garters"] as Garterbelts;
				garters.Remove(this.listview_garters.SelectedItem as GarterProcesses);
				SetProcess(null);
			}
		}
		private void TextBoxProcessName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				Button_FindProcessDialogOK_Click(null, null);
				DialogHost.CloseDialogCommand.Execute(null, null);
			}
		}
		private void Listitem_DoubleClick(object sender, MouseButtonEventArgs e)
		{
			Button_SelectDialog_Click(buttonSDOk, null);
			DialogHost.CloseDialogCommand.Execute(null, null);
		}
		private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				DialogHost.CloseDialogCommand.Execute(null, null);
			}
		}
		private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
			e.Handled = true;
		}

		#endregion

	}
}
