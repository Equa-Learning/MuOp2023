using System;
using System.Windows.Forms;

namespace MuOp2023.Practice_01_02
{
    public partial class TranslateCmToM : Form
    {
        public TranslateCmToM()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out var x))
            {
                MessageBox.Show("Неверный ввод, не удалось определить целое число");
            }
            else
            {
                var div = Math.DivRem(x, 100, out var remainder);
                MessageBox.Show($@"Расстояние равно {div} {Padezh(div,("метр","метра","метров"))} {remainder} {Padezh(remainder,("сантиметр","сантиметра","сантиметров"))}", 
                    "Результат перевода", 
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        
        private static string Padezh (int num, (string first, string second, string third) a ) {
            var n = Math.Abs(num) % 100; 
            var n1 = n % 10;
            if (n > 10 && n < 20) { return a.third; }
            if (n1 > 1 && n1 < 5) { return a.second; }
            return n1 == 1 ? a.first : a.third;
        }
    }
}