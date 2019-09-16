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
    public partial class GarterSelect : Form
    {
        List<Garterbelt> garters = FetishManager.Instance.GetFetishes();

        public GarterSelect()
        {
            InitializeComponent();

            for (int i = 0; i < garters.Count; i++)
            {
                var g = garters[i];
                listView.Items.Add(new ListViewItem(new string[] {
                    i+1.ToString(), g.Name, g.Processes[0].MainWindowHandle.ToString()
                }));
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public Garterbelt GetResult()
        {
            if (listView.SelectedIndices.Count == 0) return null;
            return garters[listView.SelectedIndices[0]];
        }
    }
}
