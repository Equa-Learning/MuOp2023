using System;
using System.Windows.Forms;

namespace MuOp2023.Practice_01_02
{
    public partial class TwoButtons : Form
    {
        public TwoButtons()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
        }
    }
}