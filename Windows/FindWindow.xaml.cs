using System;
using System.Windows;
using System.Windows.Input;

namespace GarterBelt.Windows {
    public partial class FindWindow : Window {
        public event Action<string> onConfirm;

        public FindWindow() {
            this.InitializeComponent();
            this.ConfirmButton.Click += delegate { this.confirm(); };
            KeyDown += delegate (object sender, KeyEventArgs e) {
                if (e.Key == Key.Enter) this.confirm();
            };
            this.ProcessNameText.Focus();
        }

        public void confirm() {
            this.onConfirm(this.ProcessNameText.Text);
            this.Close();
        }
    }
}
