using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GarterBelt.GUI
{
    public partial class ShortcutSelect : Form
    {
        public ShortcutSelect()
        {
            InitializeComponent();
            LoadShortcut();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SaveShortcut();
            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadShortcut()
        {
            var shortcuts = ShortCut.LoadShortcuts();
            var textboxes = new TextBox[] { text_scWindowShow, text_scWindowHide, text_scGarterShow,text_scGarterHide };
            for(int i = 0; i < textboxes.Length; i++)
            {
                var textbox = textboxes[i];
                if (i >= shortcuts.Count) break;
                textbox.Text = GetKeyString(shortcuts[i]);
            }
        }

        private void SaveShortcut()
        {
            var shortcuts = new List<Tuple<int, Keys>>();
            var textboxes = new TextBox[] { text_scWindowShow, text_scWindowHide, text_scGarterShow, text_scGarterHide };
            foreach (var textBox in textboxes)
            {
                shortcuts.Add(GetKeyTuple(textBox.Text));
            }
            ShortCut.SaveShortCuts(shortcuts);
        }

        private string GetKeyString(Tuple<int, Keys> shortcuts)
        {
            var result = string.Empty;
            if ((shortcuts.Item1 & (int)GarterBelt.ModifierKeys.Shift) != 0) result += "{SHIFT}";
            if ((shortcuts.Item1 & (int)GarterBelt.ModifierKeys.Control) != 0) result += "{CTRL}";
            if ((shortcuts.Item1 & (int)GarterBelt.ModifierKeys.Alt) != 0) result += "{ALT}";
            if ((shortcuts.Item1 & (int)GarterBelt.ModifierKeys.Win) != 0) result += "{WIN}";
            result += shortcuts.Item2;
            return result;
        }
        private Tuple<int, Keys> GetKeyTuple(string text)
        {
            var modifier = 0;
            if (text.Contains("{SHIFT}"))
            {
                modifier += (int)GarterBelt.ModifierKeys.Shift;
                text = text.Replace("{SHIFT}", "");
            }
            if (text.Contains("{CTRL}"))
            {
                modifier += (int)GarterBelt.ModifierKeys.Control;
                text = text.Replace("{CTRL}", "");
            }
            if (text.Contains("{ALT}"))
            {
                modifier += (int)GarterBelt.ModifierKeys.Alt;
                text = text.Replace("{ALT}", "");
            }
            if (text.Contains("{WIN}"))
            {
                modifier += (int)GarterBelt.ModifierKeys.Win;
                text = text.Replace("{WIN}", "");
            }
            var key = (Keys)Enum.Parse(typeof(Keys), text.Trim());

            return new Tuple<int, Keys>(modifier, key);
        }
    }
}
