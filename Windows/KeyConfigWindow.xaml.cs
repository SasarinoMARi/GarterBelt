using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using static GarterBelt.ShortCut;

namespace GarterBelt.Windows {
    /// <summary>
    /// KeyConfigWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class KeyConfigWindow : Window {
        public KeyConfigWindow() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            var shortcuts = ShortCut.LoadShortcuts();
            var textboxes = new System.Windows.Controls.TextBox[] { TargetShowKeyText, TargetHideKeyText, WindowShowKeyText, WindowHideKeyText };
            for (int i = 0; i < textboxes.Length; i++) {
                var textbox = textboxes[i];
                if (i >= shortcuts.Count) break;
                textbox.Text = GetKeyString(shortcuts[i]);
            }
        }

        private void SaveShortcut() {
            var shortcuts = new List<ShortCutObject>();
            var textboxes = new System.Windows.Controls.TextBox[] { TargetShowKeyText, TargetHideKeyText, WindowShowKeyText, WindowHideKeyText };
            foreach (var textBox in textboxes) {
                shortcuts.Add(GetKeyTuple(textBox.Text));
            }
            ShortCut.SaveShortCuts(shortcuts);
        }

        private string GetKeyString(ShortCutObject shortcuts) {
            var result = string.Empty;
            if ((shortcuts.modifierKey & (uint)ModifierKeys.Shift) != 0) result += "{SHIFT}";
            if ((shortcuts.modifierKey & (uint)ModifierKeys.Control) != 0) result += "{CTRL}";
            if ((shortcuts.modifierKey & (uint)ModifierKeys.Alt) != 0) result += "{ALT}";
            if ((shortcuts.modifierKey & (uint)ModifierKeys.Win) != 0) result += "{WIN}";
            result += (Keys)shortcuts.key;
            return result;
        }

        private ShortCutObject GetKeyTuple(string text) {
            uint modifier = 0;
            if (text.Contains("{SHIFT}")) {
                modifier += (uint)ModifierKeys.Shift;
                text = text.Replace("{SHIFT}", "");
            }
            if (text.Contains("{CTRL}")) {
                modifier += (uint)ModifierKeys.Control;
                text = text.Replace("{CTRL}", "");
            }
            if (text.Contains("{ALT}")) {
                modifier += (uint)ModifierKeys.Alt;
                text = text.Replace("{ALT}", "");
            }
            if (text.Contains("{WIN}")) {
                modifier += (uint)ModifierKeys.Win;
                text = text.Replace("{WIN}", "");
            }
            var key = (Keys)Enum.Parse(typeof(Keys), text.Trim());

            return new ShortCutObject(modifier, (uint)key);
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e) {
            this.SaveShortcut();
            this.Close();
        }
    }
}
