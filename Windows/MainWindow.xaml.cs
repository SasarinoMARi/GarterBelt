using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;

namespace GarterBelt.Windows {
    public partial class MainWindow : Window {
        readonly List<Garterbelt> SelectedGarters = new List<Garterbelt>();
        readonly GlobalHotkeyBinder KeyBinder = new GlobalHotkeyBinder();

        public MainWindow() {
            this.InitializeComponent();
            this.initializeHotKey();
            this.initializeButtonEvent();

            this.updateProcessContainer();
            this.loadActiveState();
            this.Closing += delegate {
                this.saveActiveState();
            };
        }


        private void initializeButtonEvent() {
            this.ShowButton.Click += delegate {
                this.showFetishes();
            };
            this.HideButton.Click += delegate {
                this.hideFetishes();
            };
            this.TopMostButton.Click += delegate {
                this.setTopMostFetishes(true);
            };
            this.TopMostDisableButton.Click += delegate {
                this.setTopMostFetishes(false);
            };

            this.FindButton.Click += delegate {
                var w = new FindWindow();
                w.onConfirm += delegate (string processName) {
                    var newFetishe = FetishManager.Instance.find(processName);
                    this.updateProcessContainer();
                    if (this.ProcessContainer.Children[this.ProcessContainer.Children.Count - 1] is ProcessView view) {
                        view.enabled = true;
                    }
                };
                w.ShowDialog();
            };

            this.ClearButton.Click += delegate {
                FetishManager.Instance.clear();
                SelectedGarters.Clear();
                this.updateProcessContainer();
            };

            this.OpacitySlider.ValueChanged += delegate (object sender, RoutedPropertyChangedEventArgs<double> e) {
                this.setOpacityFetishes((int)Math.Round(e.NewValue));
            };

            this.KeyConfigButton.Click += delegate {
                var window = new KeyConfigWindow();
                window.ShowDialog();
                this.initializeHotKey();
            };
        }

        private void initializeHotKey() {
            this.KeyBinder.Clear();

            var sc = ShortCut.LoadShortcuts();
            var i = 0;
            this.KeyBinder.AddBindHandler(sc[i].key, sc[i++].modifierKey, delegate (object sender, KeyPressedEventArgs e) {
                this.showFetishes();
            });
            this.KeyBinder.AddBindHandler(sc[i].key, sc[i++].modifierKey, delegate (object sender, KeyPressedEventArgs e) {
                this.hideFetishes();
            });
            this.KeyBinder.AddBindHandler(sc[i].key, sc[i++].modifierKey, delegate (object sender, KeyPressedEventArgs e) {
                this.Show();
            });
            this.KeyBinder.AddBindHandler(sc[i].key, sc[i++].modifierKey, delegate (object sender, KeyPressedEventArgs e) {
                this.Hide();
            });
        }

        private void updateProcessContainer() {
            this.ProcessContainer.Children.Clear();
            foreach (var fetishe in FetishManager.Instance.get()) {
                var view = new ProcessView(fetishe);
                view.stateChanged += this.processStateChanged;
                if (SelectedGarters.Exists(it => it.Name == fetishe.Name)) view.enabled = true;
                this.ProcessContainer.Children.Add(view);
            }
        }

        private void processStateChanged(Garterbelt fetishe, bool enabled) {
            if (enabled) {
                if (!this.SelectedGarters.Exists(it => it.Name == fetishe.Name))
                    this.SelectedGarters.Add(fetishe);
            }
            else this.SelectedGarters.RemoveAll(it => it.Name == fetishe.Name);
        }

        #region 페티시 상호작용
        private void showFetishes() => this.SelectedGarters.ForEach(it => it.Show());
        private void hideFetishes() => this.SelectedGarters.ForEach(it => it.Hide());
        private void setTopMostFetishes(bool state) => this.SelectedGarters.ForEach(it => it.SetTopmost(state));
        private void setOpacityFetishes(int opacity) => this.SelectedGarters.ForEach(it => it.SetOpacity(opacity));
        #endregion

        #region File I/O
        private static readonly GarterStatePreference preference = new GarterStatePreference();
        private class GarterStatePreference {
            private static readonly string saveDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Assembly.GetEntryAssembly().GetName().Name);
            private static readonly string savePath = Path.Combine(saveDir, "activeState");

            public string[] loadActiveFetishes() {
                if (!File.Exists(savePath)) return new string[0];
                var saved = File.ReadAllLines(savePath);
                return saved ?? (new string[0]);
            }

            public void saveActiveFetishes(List<Garterbelt> fetishes) {
                var str = "";
                fetishes.ForEach(it => str += $"{it}\n");
                File.WriteAllText(savePath, str.Trim());
            }
        }
        private void loadActiveState() {
            var fetishes = FetishManager.Instance.get();
            var result = preference.loadActiveFetishes();
            foreach (var name in result) fetishes
                .FindAll(it => it.Name == name)
                .ForEach(it => this.processStateChanged(it, true));
            this.updateProcessContainer();
        }

        private void saveActiveState() => preference.saveActiveFetishes(this.SelectedGarters);
        #endregion
    }
}
