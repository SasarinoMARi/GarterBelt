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
    public partial class GarterGUI : Form
    {
        public GarterGUI()
        {
            InitializeComponent();
            ApplyShortcuts();
        }

        Garterbelt selectedG = null;
        GlobalHotkeyBinder binder = new GlobalHotkeyBinder();

        private void ApplyShortcuts()
        {
            var sc = ShortCut.LoadShortcuts();
            var i = 0;
            binder.AddBindHandler("garter" + i, sc[i].Item2, sc[i++].Item1, delegate (object sender, KeyPressedEventArgs e)
            {
                selectedG?.Show();
            });
            binder.AddBindHandler("garter" + i, sc[i].Item2, sc[i++].Item1, delegate (object sender, KeyPressedEventArgs e)
            {
                selectedG?.Hide();
            });
            binder.AddBindHandler("garter" + i, sc[i].Item2, sc[i++].Item1, delegate (object sender, KeyPressedEventArgs e)
            {
                this.Show();
            });
            binder.AddBindHandler("garter" + i, sc[i].Item2, sc[i++].Item1, delegate (object sender, KeyPressedEventArgs e)
            {
                this.Hide();
            });
        }

        private void button_findHandle_Click(object sender, EventArgs e)
        {
            var result = string.Empty;
            if (ShowInputBox("", Properties.Resources.InputProcessName, ref result) != DialogResult.OK) return;
            selectedG = FetishManager.Instance.FindFetish(result);
        }

        private void button_selectHandle_Click(object sender, EventArgs e)
        {
            var selector = new GarterSelect();
            if (selector.ShowDialog() != DialogResult.OK) return;
            selectedG = selector.GetResult();
        }

        private static DialogResult ShowInputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private void button_Show_Click(object sender, EventArgs e)
        {
            selectedG?.Show();
        }

        private void button_Hide_Click(object sender, EventArgs e)
        {
            selectedG?.Hide();
        }

        private void button_Opacity_Click(object sender, EventArgs e)
        {
            //selectedG?.SetOpacity
        }

        private void button_Topmost_Click(object sender, EventArgs e)
        {
            selectedG.SetTopmost(true);
        }

        private void button_Shortcuts_Click(object sender, EventArgs e)
        {
            var selector = new ShortcutSelect();
            if (selector.ShowDialog() != DialogResult.OK) return;
            ApplyShortcuts();
        }
    }

}
