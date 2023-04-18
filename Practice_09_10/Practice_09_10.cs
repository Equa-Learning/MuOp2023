using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MuOp2023.Infrastructure;


namespace MuOp2023.Practice_09_10
{
    public partial class Practice0910 : Form
    {
        public Practice0910()
        {
            InitializeComponent();
            comboBox1.DataSource = GetTaskDescriptions();
            
        }
        
        private enum Tasks
        {
            [Description("Задача не задана")] NotSet,
            [Description("Определение характеристик числа")] NumberProperties,
            //[Description("Числа, заканчивающиеся на 4")] EndOn4,
        }

        private delegate void ComputeDelegate();

        private ComputeDelegate _action;
        private TextBox[] TextBoxes => new[] { textBox1, textBox2, textBox3, };
        private Label[] Labels => new[] { label1, label2, label3 };

        private readonly TextBoxCheckDelegate[] Checks = new TextBoxCheckDelegate[3]
            { TextBoxValidators.AlwaysOk, TextBoxValidators.AlwaysOk, TextBoxValidators.AlwaysOk };

        private void SetupFieldsForTask(string[] labelsForTexBoxes, List<TextBoxCheckDelegate> checksForInputs)
        {
            TextBoxService.CleanFields(TextBoxes);
            TextBoxService.InitFields(TextBoxes, Labels, labelsForTexBoxes);
            TextBoxValidatorService.SetupChecks(Checks, checksForInputs);
        }

        private static string[] GetTaskDescriptions()
        {
            return Enum.GetValues(typeof(Tasks)).Cast<Tasks>().Select(x => x.GetDescription()).ToArray();
        }

        private void computeButton_Click(object sender, EventArgs e)
        {
            if (!TextBoxValidatorService.RunChecks(Checks, TextBoxes, resultLabel))
                return;

            _action();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedName = ((string) comboBox1.SelectedItem).ToEnumOrDefault<Tasks>();
            var labelsForTexBoxes = Array.Empty<string>();
            var checksForInputs = new List<TextBoxCheckDelegate>();
            computeButton.Text = "Вычислить";
            cleanButton.Visible = false;
            pictureBox1.Visible = false;
            listBox1.Visible = false;
            resultLabel.Text = "";

            switch (selectedName)
            {
                case Tasks.NotSet:
                    taskDefinition.Text =
                        "Задача не выбрана.\n" +
                        "Выберите задачу в выпадающем поле вверху";
                    _action = () => resultLabel.Text = "Задача не выбрана, нечего расчитывать";
                    break;
                case Tasks.NumberProperties:
                    labelsForTexBoxes = Task_NumberProperties(checksForInputs);
                    break;
              
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SetupFieldsForTask(labelsForTexBoxes, checksForInputs);
        }
        
        private string[] Task_NumberProperties(List<TextBoxCheckDelegate> checksForInputs)
        {
            taskDefinition.Text =
                $"Найти для числа А: Количество цифр; Сумму числа; Первую цифру числа; " +
                $"Количество четных цифр числа; самую большую цифру; сумму цифр больших N;" +
                $"сколько раз N встречается в числе; обратный порядок.";
            var labelsForTexBoxes = new[] { "Введите число A", "Введите число N" };
            checksForInputs.AddRange(new TextBoxCheckDelegate[] {
                TextBoxValidators.IsPositiveNumber, 
                (input, log) =>
                uint.TryParse(input.Text, NumberStyles.Number, TextBoxValidatorService.RuCultureInfo, out var result) 
                    && result <10
                    ? true
                    : (log.Text += $"{input.Name} не положительное число\n") is null
            });
            _action = () =>
            {
                var numberA = int.Parse(textBox1.Text);
                var numberN = uint.Parse(textBox2.Text);

                var cipherQuantity = Convert.ToInt32(Math.Floor(Math.Log10(numberA)));
                int firstCipher =-1, evenSum=0, biggestCipher=0, sumOverN=0, cipherTimes=0, reversed = 0;
                
                var cipherSum = 0;
                for (var i = cipherQuantity; i >= 0; i--) {
                    var divResult = Math.DivRem(numberA, (int) Math.Pow(10, i), out numberA);
                    if (firstCipher < 0) firstCipher = divResult;
                    if (divResult % 2 == 0) evenSum ++;
                    if (divResult > biggestCipher) biggestCipher = divResult;
                    if (divResult > numberN) sumOverN += divResult;
                    if (divResult == numberN) cipherTimes++;
                    reversed += divResult * (int)Math.Pow(10, cipherQuantity - i);
                    cipherSum += divResult;
                }

                resultLabel.Text = $"Количество цифр {cipherQuantity+1}; Сумму цифр числа {cipherSum}; Первую цифру числа {firstCipher}; " +
                                   $"Количество четных цифр числа {evenSum}; самую большую цифру числа {biggestCipher}; найти сумму цифр числа, больших N {sumOverN};" +
                                   $"сколько раз данная цифра встречается в числе {cipherTimes}; обратный порядок цифр {reversed}";
            };
            return labelsForTexBoxes;
        }



    }
}