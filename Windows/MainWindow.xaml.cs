using GarterBelt.GUI;
using System;
using System.Collections.Generic;
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
        }

        private void initializeHotKey() {
            var sc = ShortCut.LoadShortcuts();
            var i = 0;
            this.KeyBinder.AddBindHandler("garter" + i, sc[i].Item2, sc[i++].Item1, delegate (object sender, KeyPressedEventArgs e) {
                this.showFetishes();
            });
            this.KeyBinder.AddBindHandler("garter" + i, sc[i].Item2, sc[i++].Item1, delegate (object sender, KeyPressedEventArgs e) {
                this.hideFetishes();
            });
            this.KeyBinder.AddBindHandler("garter" + i, sc[i].Item2, sc[i++].Item1, delegate (object sender, KeyPressedEventArgs e) {
                this.setTopMostFetishes(true);
            });
            this.KeyBinder.AddBindHandler("garter" + i, sc[i].Item2, sc[i++].Item1, delegate (object sender, KeyPressedEventArgs e) {
                this.setTopMostFetishes(false);
            });
        }

        private void updateProcessContainer() {
            this.ProcessContainer.Children.Clear();
            foreach (var fetishe in FetishManager.Instance.get()) {
                var view = new ProcessView(fetishe);
                view.stateChanged += this.processStateChanged;
                if (SelectedGarters.Exists(it=>it.Name == fetishe.Name)) view.enabled = true;
                this.ProcessContainer.Children.Add(view);
            }
        }

        private void processStateChanged(Garterbelt fetishe, bool enabled) {
            if (enabled) {
                if(!this.SelectedGarters.Exists(it => it.Name == fetishe.Name))
                    this.SelectedGarters.Add(fetishe);
            }
            else this.SelectedGarters.RemoveAll(it => it.Name == fetishe.Name);
        }

        #region 페티시 상호작용
        private void showFetishes() => this.SelectedGarters.ForEach(it => it.Show());
        private void hideFetishes() => this.SelectedGarters.ForEach(it => it.Hide());
        private void setTopMostFetishes(bool state) => this.SelectedGarters.ForEach(it => it.SetTopmost(state));
        #endregion
    }
}
