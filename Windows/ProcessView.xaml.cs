using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GarterBelt.Windows {
    /// <summary>
    /// ProcessView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProcessView : UserControl {
        private readonly Garterbelt fetishe;

        public ProcessView(Garterbelt fetishe) {
            this.InitializeComponent();

            this.fetishe = fetishe;
            this.ProcessName.Content = fetishe.Name;
            // TODO: 프로세스 아이콘 가져오기

            this.ProcessEnabled.Checked += ProcessEnabled_ChangeState;
            this.ProcessEnabled.Unchecked += ProcessEnabled_ChangeState;
        }

        private void ProcessEnabled_ChangeState(object sender, RoutedEventArgs e) =>
            stateChanged?.Invoke(this.fetishe, this.enabled);

        public bool enabled {
            get {
                var c = this.ProcessEnabled.IsChecked;
                return c ?? false;
            }
            set => this.ProcessEnabled.IsChecked = value;
        }

        public event Action<Garterbelt, bool> stateChanged;
    }
}
