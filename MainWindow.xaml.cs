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
        Garterbelt garter = null;
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

        private void SetProcess(Garterbelt g)
        {
            this.garter = g;
            if (g == null)
            {
                Console.WriteLine("선택된 프로세스가 없습니다.");
                this.labelHandleId.Content = "선택된 프로세스가 없습니다.";
                this.sliderOpacity.Value = this.sliderOpacity.Maximum;
                this.panelMainControls.IsEnabled = false;
                ExportHandle();
            }
            else
            {
                Console.WriteLine(string.Format("프로세스 {0} 대상으로 garterbelt를 초기화합니다.", g.Name));
                this.labelHandleId.Content = "활성화된 프로세스 : " + g.Name;
                var opacity = WindowsProperty.SetWindowToStyled(garter.Processes[0].MainWindowHandle);
                this.sliderOpacity.Value = opacity;
                if (!this.panelMainControls.IsEnabled) this.panelMainControls.IsEnabled = true;
                ExportHandle();
            }
        }
        private void FindHandle(string name)
        {
            name = name.ToLower();
            Process[] processRunning = Process.GetProcesses();
            ObservableGarterbelt garters = Resources["garters"] as ObservableGarterbelt;
            foreach (Process p in processRunning)
            {
                if (p.MainWindowHandle.ToInt32() == 0) continue;
                if (p.ProcessName.ToLower().Contains(name))
                {
                    if (garters.Any(x => x.ContainProcess(p)))
                    {
                        continue;
                    }
                    else
                    {
                        var query = garters.Where(x => x.Name == p.ProcessName);
                        if (query.Count() > 0)
                        {
                            query.First().AttatchGarterbelt(p);
                            SetProcess(query.First());
                        }
                        else
                        {
                            var g = new Garterbelt(p);
                            garters.Add(g);
                            SetProcess(g);
                        }
                    }
                }
            }
        }

        string filepath = "id";

        private void ExportHandle()
        {
            ObservableGarterbelt garters = Resources["garters"] as ObservableGarterbelt;
            Garterbelt.SerializeObject(garters.ToList(), filepath);
            Console.WriteLine("session export successfully.");
        }
        private void LoadHandle()
        {
            if (!File.Exists(filepath)) return;
            ObservableGarterbelt garters = Resources["garters"] as ObservableGarterbelt;
            var saved = Garterbelt.DeserializeObject(filepath);
            if (saved == null || saved.Count == 0) return;
            Garterbelt recent = saved[0];
            foreach (var garter in saved)
            {
                if (!ValidateHandle(garter)) continue;
                //if (garters.Any(x => x.ContainProcess(garter) == garter.ProcessId && x.Name == garter.Name)) continue;
                garters.Add(garter);
            }
            Console.WriteLine("session import successfully.");
            Console.WriteLine(string.Format("number of GarterProcess which imported from file : {0}", garters.Count.ToString()));
            Console.WriteLine(string.Format("recent activated GarterProcess : {0}", recent.ToString()));
            if (garters.Count > 0)
            {
                //var query = garters.Where(item => item.MainWindowHandle == recent.MainWindowHandle);
                //if (query.Count() > 0) SetProcess(query.First());
                //else 
                SetProcess(garters.First());
            }
        }
        private bool ValidateHandle(Garterbelt garter)
        {
            try
            {
                foreach (var x in garter.Processes)
                {
                    var p = Process.GetProcessById(x.ProcessId);
                    if (p != null) return true;
                }
            }
            catch
            {

            }
            return false;
        }

        private void ShowByHandle()
        {
            if (garter == null) return;
            foreach (var p in garter.Processes)
            {
                WindowsProperty.ShowWindow(p.MainWindowHandle);
            }
        }
        private void HideByHandle()
        {
            if (garter == null) return;
            foreach (var p in garter.Processes)
            {
                WindowsProperty.HideWindow(p.MainWindowHandle);
            }
        }
        private void DisableTopmostByHandle()
        {
            if (garter == null) return;
            foreach (var p in garter.Processes)
            {
                WindowsProperty.SetTopmost(p.MainWindowHandle, false);
            }
        }
        private void TopmostByHandle()
        {
            if (garter == null) return;
            foreach (var p in garter.Processes)
            {
                WindowsProperty.SetTopmost(p.MainWindowHandle, true);
            }
        }
        private void OpacityByHandle(byte opacity)
        {
            if (garter == null) return;
            foreach (var p in garter.Processes)
            {
                WindowsProperty.SetOpacity(p.MainWindowHandle, opacity);
            }
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
                var g = this.listview_garters.SelectedItem as Garterbelt;
                if (g == null) return;
                SetProcess(g);
            }
            if (sender == this.buttonSDRemove)
            {
                ObservableGarterbelt garters = Resources["garters"] as ObservableGarterbelt;
                garters.Remove(this.listview_garters.SelectedItem as Garterbelt);
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
