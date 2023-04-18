using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MuOp2023.Infrastructure;

namespace MuOp2023.Practice_07_08
{
    public partial class Practice0708 : Form
    {
        public Practice0708()
        {
            InitializeComponent();
            comboBox1.DataSource = GetTaskDescriptions();
        }

        private enum Tasks
        {
            [Description("Задача не задана")] NotSet,
            [Description("Белоснежка и рубашки гномам")] Snowhite,
            [Description("Числа, заканчивающиеся на 4")] EndOn4,
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
                case Tasks.Snowhite:
                    labelsForTexBoxes = Task_ShowWhiteAndSorochka(checksForInputs);
                    break;
                case Tasks.EndOn4:
                    labelsForTexBoxes = Task_EndOn4(checksForInputs);
                    break;                
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SetupFieldsForTask(labelsForTexBoxes, checksForInputs);
        }

        private string[] Task_EndOn4(List<TextBoxCheckDelegate> checksForInputs)
        {
            taskDefinition.Text = 
                "Составить программу выводящую список " +
                "всех чисел, заканчивающихся на цифру X, от 1 до 100";
            var labelsForTexBoxes = new[] { "На какую цифру должны заканчиваться числа?" };
            checksForInputs.AddRange(new TextBoxCheckDelegate[]
            {
                ( input, log) =>
                    uint.TryParse(input.Text, NumberStyles.Number, TextBoxValidatorService.RuCultureInfo, out var result)
                    && result < 10
                        ? true
                        : (log.Text += $"{input.Name} не положительное число от 0 до 9\n") is null
            });
            
            _action = () =>
            {
                var cipher = uint.Parse(textBox1.Text);
                var result = new List<int>();
                for (var i = 1; i <= 100; i++)
                {
                    if (i % 10 == cipher)
                        result.Add(i);
                }
                var message = string.Join(", ", result);
                resultLabel.Text += $"Цифры, заканчивающиеся на {cipher} от 1 до 100, вот они:\n" +
                                    $"{message}";
            };
            return labelsForTexBoxes;
        }
        
        private string[] Task_ShowWhiteAndSorochka(List<TextBoxCheckDelegate> checksForInputs)
        {
            taskDefinition.Text =
                "Белоснежка на Рождество решила купить нескольким гномам новые рубашки и измерила рост каждого. " +
                "В магазине продаются рубашки трех ростов: I — 2535 см, II — 3645 см и III — 4655 см. " +
                "Выведите таблицу, в которой будут указаны реальный рост каждого гнома и соответ-ствующий ему рост рубашки.";
            var labelsForTexBoxes = new[] { "Скольким гномам покупаем рубашки ? 1-10" };
            checksForInputs.AddRange(new TextBoxCheckDelegate[]
            {
                ( input, log) =>
                    uint.TryParse(input.Text, NumberStyles.Number, TextBoxValidatorService.RuCultureInfo,
                        out var result)
                    && result >= 1 && result <= 10
                        ? true
                        : (log.Text += $"{input.Name} не положительное число от 1 до 10\n") is null
            });
            var sizes = new List<(int lower, int higher)>() { (25, 35), (36, 45), (46, 55) };
            _action = () =>
            {
                var dwarfesCount = uint.Parse(textBox1.Text);
                var dwarfesLengths = new uint[dwarfesCount];
                var rnd = new Random();
                for (var i = 0; i < dwarfesCount; i++)
                {
                    dwarfesLengths[i] = (uint) rnd.Next(25, 55);
                }

                resultLabel.Text = $"Измеренная высота гномов: {string.Join(", ", dwarfesLengths)}\n";
                var fitSorochka = new Dictionary<(int lower, int higher), int>();
                foreach (var (lower, higher) in sizes)
                {
                    fitSorochka[(lower, higher)]
                        = dwarfesLengths.Count(x => x >= lower && x <= higher);
                }

                var message = string.Join(", ",
                    fitSorochka
                        .Where(x => x.Value > 0)
                        .Select(x =>
                            $"от {x.Key.lower} до {x.Key.higher} см - {x.Value} шт."));
                resultLabel.Text += $"Нужно сорочек: {message}\n";
            };
            return labelsForTexBoxes;
        }
        
        
    }
}