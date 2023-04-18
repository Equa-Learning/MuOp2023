using System;
using System.Drawing;
using System.Windows.Forms;

namespace MuOp2023.Practice_01_02
{
    public partial class DifferentColors : Form
    {
        public DifferentColors()
        {
            InitializeComponent();
            textBox1.Text = "Кнопки ещё не были нажаты";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Щелчок на кнопке 1";
            textBox1.ForeColor = Color.Red;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Щелчок на кнопке 2";
            textBox1.ForeColor = Color.Blue;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Щелчок на кнопке 3";
            textBox1.ForeColor = Color.Green;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Щелчок на кнопке 4";
            textBox1.ForeColor = Color.Magenta;
        }


    }
}