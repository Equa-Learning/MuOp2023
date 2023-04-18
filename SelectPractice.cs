using System;
using System.Windows.Forms;
using MuOp2023.Practice_01_02;
using MuOp2023.Practice_07_08;

namespace MuOp2023
{
    public partial class SelectPractice : Form
    {
        public SelectPractice()
        {
            InitializeComponent();
        }

        private void practice_01_02_Click(object sender, EventArgs e)
        {
            var mainTaskForm = new Practice_01_02.Practice0102();
            mainTaskForm.Show();
        }

        private void practice_03_04_Click(object sender, EventArgs e)
        {
            var mainTaskForm = new Practice_03_04.Practice0304();
            mainTaskForm.Show();
        }

        private void practice_05_06_Click(object sender, EventArgs e)
        {
            var mainTaskForm = new Practice_05_06.Practice0506();
            mainTaskForm.Show();
        }

        private void practice_07_08_Click(object sender, EventArgs e)
        {
            var mainTaskForm = new Practice_07_08.Practice0708();
            mainTaskForm.Show();
        }
    }
}