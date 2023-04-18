using System;
using System.Windows.Forms;

namespace MuOp2023.Practice_01_02
{
    public partial class Practice0102 : Form
    {
        public Practice0102()
        {
            InitializeComponent();
        }

        private void TranslateFtoCForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        private void Rerun(object sender, EventArgs e)
        {
        
        }

        private void TranslateFtoC(object sender, EventArgs e)
        {
            try
            {
                var fahrenheight = Convert.ToDouble(textBox1.Text);
                var celsius = 5.0 / 9 * (fahrenheight - 32);
                label1.Text = $"Температура по Целькию равна {celsius:F2}";
            }
            catch 
            {
                MessageBox.Show("Неверный ввод. Не удалось распознать целое число.");
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            var twoButtons = new TwoButtons();
            twoButtons.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var diffColors = new DifferentColors();
            diffColors.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var diffButtons = new DifferentButtons();
            diffButtons.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var translateCmToM = new TranslateCmToM();
            translateCmToM.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var selfTasks = new SelfTasks();
            selfTasks.Show();        
        }
    }
}