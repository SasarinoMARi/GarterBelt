using GarterBelt.GUI;
using System;
using System.Windows;

namespace GarterBelt.Windows
{
    public partial class MainWindow : Window
    {
        Garterbelt SelectedGarter = null;
        readonly GlobalHotkeyBinder KeyBinder = new GlobalHotkeyBinder();

        public MainWindow()
        {
            this.InitializeComponent();
            this.initializeHotKey();
            this.initializeButtonEvent();
        }

        private void initializeButtonEvent()
        {
            this.ShowButton.Click += delegate {
                this.SelectedGarter?.Show();
            };
            this.HideButton.Click += delegate {
                this.SelectedGarter?.Hide();
            };
            this.TopMostButton.Click += delegate {
                this.SelectedGarter?.SetTopmost(true);
            };
            this.TopMostDisableButton.Click += delegate {
                this.SelectedGarter?.SetTopmost(false);
            };

            this.FindButton.Click += delegate
            {
                var w = new FindWindow();
                w.onConfirm += delegate (string processName)
                {
                    this.SelectedGarter = FetishManager.Instance.FindFetish(processName);
                };
                w.ShowDialog();
            };
        }

        private void initializeHotKey()
        {
            var sc = ShortCut.LoadShortcuts();
            var i = 0;
            this.KeyBinder.AddBindHandler("garter" + i, sc[i].Item2, sc[i++].Item1, delegate (object sender, KeyPressedEventArgs e)
            {
                SelectedGarter?.Show();
            });
            this.KeyBinder.AddBindHandler("garter" + i, sc[i].Item2, sc[i++].Item1, delegate (object sender, KeyPressedEventArgs e)
            {
                SelectedGarter?.Hide();
            });
            this.KeyBinder.AddBindHandler("garter" + i, sc[i].Item2, sc[i++].Item1, delegate (object sender, KeyPressedEventArgs e)
            {
                this.Show();
            });
            this.KeyBinder.AddBindHandler("garter" + i, sc[i].Item2, sc[i++].Item1, delegate (object sender, KeyPressedEventArgs e)
            {
                this.Hide();
            });
        }

    }
}
