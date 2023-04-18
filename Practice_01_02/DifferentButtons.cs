using System;
using System.Windows.Forms;

namespace MuOp2023.Practice_01_02
{
    public partial class DifferentButtons : Form
    {
        public DifferentButtons()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Текст выведен в метку";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "";
        }        


        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
        }        
    }
}