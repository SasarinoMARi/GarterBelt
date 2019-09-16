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
using Application = System.Windows.Application;

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
            InitializeComponent();

            #region key binding
            binder.AddBindHandler("windowHideHandler", Keys.F1, ModifierKeys.Shift, delegate (object sender, KeyPressedEventArgs e)
            {
                garter?.Hide();
            });
            binder.AddBindHandler("windowShowHandler", Keys.F2, ModifierKeys.Shift, delegate (object sender, KeyPressedEventArgs e)
            {
                garter?.Show();
            });
            binder.AddBindHandler("garterHideHandler", Keys.F1, ModifierKeys.Control, delegate (object sender, KeyPressedEventArgs e)
            {
                this.Hide();
            });
            binder.AddBindHandler("garterShowHandler", Keys.F2, ModifierKeys.Control, delegate (object sender, KeyPressedEventArgs e)
            {
                this.Show();
            });
            binder.AddBindHandler("garterToggleConsoleHandler", Keys.F3, ModifierKeys.Control, delegate (object sender, KeyPressedEventArgs e)
            {
                ConsoleManager.Toggle();
            });
            #endregion
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
            }
            else
            {
                Console.WriteLine(string.Format("프로세스 {0} 대상으로 garterbelt를 초기화합니다.", g.Name));
                this.labelHandleId.Content = "활성화된 프로세스 : " + g.Name;
                var opacity = WindowsProperty.SetWindowToStyled(garter.Processes[0].MainWindowHandle);
                this.sliderOpacity.Value = opacity;
                if (!this.panelMainControls.IsEnabled) this.panelMainControls.IsEnabled = true;
            }
        }
        private void FindHandle(string name)
        {
            var garter = FetishManager.Instance.FindFetish(name);
            SetProcess(garter);
        }

        private void Button_Click_Common(object sender, RoutedEventArgs e)
        {
            if (sender == this.buttonHideByHandle) garter?.Hide();
            if (sender == this.buttonShowByHandle) garter?.Show();

            if (sender == this.buttonTopmost) garter?.SetTopmost(true);
            if (sender == this.buttonNoTopmost) garter?.SetTopmost(false);
        }
        private void SliderOpacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            garter?.SetOpacity(byte.Parse(((int)this.sliderOpacity.Value).ToString()));
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
                FetishManager.Instance.RemoveFetish(listview_garters.SelectedItem as Garterbelt);
                SetProcess(null);
            }
        }
        private void TextBoxProcessName_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Button_FindProcessDialogOK_Click(null, null);
                closeAllDialogs();
            }
        }
        private void Listitem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Button_SelectDialog_Click(buttonSDOk, null);
            closeAllDialogs();
        }
        private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                closeAllDialogs();
            }
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void closeAllDialogs()
        {
            // RoutedCommand(DialogHost.CloseDialogCommand) does not work after libraries merged.
            // DialogHost.CloseDialogCommand.Execute(null, null);

            searchWIndow.IsOpen = false;
            listWindow.IsOpen = false;
            informationWindow.IsOpen = false;
        }

    }
}
