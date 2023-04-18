using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using MuOp2023.Infrastructure;

namespace MuOp2023.Practice_01_02
{
    public partial class SelfTasks : Form
    {
        public SelfTasks()
        {
            InitializeComponent();
        }

        private enum Tasks
        {
            NotSet,
            SumAndMultiply,
            PerimeterAndSquare,
            CubeVAndSquare,
            Average,
            Candies,
            Kilobytes,
            SumAndMultiplyDigits,
            FullHoursSinceMidnight,
            FullMinutesSinceLastHour,
            RectanglePerimeter,
        }

        private IReadOnlyList<TextBox> Fields => new TextBox[3] { textBox1, textBox2, textBox3 };
        private IReadOnlyList<Label> Labels => new Label[3] { label1, label2, label3 };

        private Tasks _currentTask = Tasks.NotSet;
        private Tasks CurrentTask
        {
            get => _currentTask;
            set
            {
                _currentTask = value;
                switch (_currentTask)
                {
                    case Tasks.SumAndMultiply:
                        taskTitle.Text = "Даны три целых числа. Найти их сумму и произведение.";
                        InitFields( new[] { "Число А", "Число Б", "Число В" });
                        TextBoxValidatorService.SetupChecks(Checks,
                            new List<TextBoxCheckDelegate>()
                                { TextBoxValidators.IsNumber, TextBoxValidators.IsNumber, TextBoxValidators.IsNumber });
                        break;
                    case Tasks.PerimeterAndSquare:
                        taskTitle.Text = "Дана сторона квадрата a. Найти его периметр и площадь.";
                        InitFields(new[] { "Сторона квадрата" });
                        TextBoxValidatorService.SetupChecks(Checks,
                            new List<TextBoxCheckDelegate>() { TextBoxValidators.IsPositiveNumber });
                        break;
                    case Tasks.CubeVAndSquare:
                        taskTitle.Text = "Дана длина ребра куба a. Найти объем куба и площадь его полной поверхности.";
                        InitFields( new[] { "Длина ребра куба" });
                        TextBoxValidatorService.SetupChecks(Checks,
                            new List<TextBoxCheckDelegate>() { TextBoxValidators.IsPositiveNumber });
                        break;
                    case Tasks.Average:
                        taskTitle.Text = "Даны три числа. Найти их среднее арифметическое.";
                        InitFields( new[] { "Число А", "Число Б", "Число В" });
                        TextBoxValidatorService.SetupChecks(Checks,
                            new List<TextBoxCheckDelegate>()
                                { TextBoxValidators.IsNumber, TextBoxValidators.IsNumber, TextBoxValidators.IsNumber });
                        break;
                    case Tasks.Candies:
                        taskTitle.Text =
                            "Известно, что X  кг конфет стоит A рублей. Определить, сколько стоит Y кг этих же конфет.";
                        InitFields( new[] { "X кг конфет", "стоят А рублей", "Y кг" });
                        TextBoxValidatorService.SetupChecks(Checks,
                            new List<TextBoxCheckDelegate>()
                            {
                                TextBoxValidators.IsPositiveNumber, TextBoxValidators.IsPositiveNumber, TextBoxValidators.IsPositiveNumber
                            });
                        break;
                    case Tasks.Kilobytes:
                        taskTitle.Text = "Дан размер файла в байтах. Переведите эту величину в килобайты.";
                        InitFields( new[] { "Размер файла в байтах" });
                        TextBoxValidatorService.SetupChecks(Checks,
                            new List<TextBoxCheckDelegate>() { TextBoxValidators.IsPositiveNumber });
                        break;
                    case Tasks.SumAndMultiplyDigits:
                        taskTitle.Text = "Дано двузначное число. Найдите сумму и произведение его цифр.";
                        InitFields(new[] { "Двузначное число" });
                        TextBoxValidatorService.SetupChecks(Checks, new List<TextBoxCheckDelegate>()
                        {
                            (input, log) =>
                                (uint.TryParse(input.Text, out var num) && Math.Ceiling((decimal) Math.Log10(num)) == 2)
                                    ? true
                                    : (log.Text += $"{input.Name} не двузначное число\n") == null
                        });
                        break;
                    case Tasks.FullHoursSinceMidnight:
                        taskTitle.Text =
                            "С начала суток прошло N секунд. Определите сколько полных часов прошло с начала суток.";
                        InitFields(new[] { "Секунд с начала суток" });
                        TextBoxValidatorService.SetupChecks(Checks, new List<TextBoxCheckDelegate>()
                        {
                            (input, log) =>
                                (uint.TryParse(input.Text, out var num) && num < 60 * 60 * 24)
                                    ? true
                                    : (log.Text += $"{input.Name} не положительное меньше {60 * 60 * 24}\n") == null
                        });
                        break;
                    case Tasks.FullMinutesSinceLastHour:
                        taskTitle.Text =
                            "С начала суток прошло N секунд. Определите сколько полных минут прошло с начала очередного часа;";
                        InitFields(new[] { "Секунд с начала суток" });
                        TextBoxValidatorService.SetupChecks(Checks, new List<TextBoxCheckDelegate>()
                        {
                            (input, log) =>
                                (uint.TryParse(input.Text, out var num) && num < 60 * 60 * 24)
                                    ? true
                                    : (log.Text += $"{input.Name} не положительное меньше {60 * 60 * 24}\n") == null
                        });
                        break;
                    case Tasks.RectanglePerimeter:
                        taskTitle.Text =
                            "Составьте программу для вычисления периметра прямоугольника. Поддерживает не целые числа";
                        InitFields(new[] { "Одна сторона прямоугольника", "Другая сторона прямоугольника" });
                        TextBoxValidatorService.SetupChecks(Checks,
                            new List<TextBoxCheckDelegate>() { TextBoxValidators.IsPositiveDecimal, TextBoxValidators.IsPositiveDecimal });
                        break;
                    default:
                        taskTitle.Text = "Такая задача неизвестна";
                        break;
                }
            }
        }

        private void InitFields(string[] strings)
        {
            TextBoxService.InitFields(Fields, Labels, strings);
            result.Text = "";
        }


        private TextBoxCheckDelegate[] Checks = new TextBoxCheckDelegate[3]
            { TextBoxValidators.AlwaysOk, TextBoxValidators.AlwaysOk, TextBoxValidators.AlwaysOk };


        private void button1_Click(object sender, EventArgs e)
        {
            CurrentTask = Tasks.SumAndMultiply;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CurrentTask = Tasks.PerimeterAndSquare;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CurrentTask = Tasks.CubeVAndSquare;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CurrentTask = Tasks.Average;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CurrentTask = Tasks.Candies;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CurrentTask = Tasks.Kilobytes;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CurrentTask = Tasks.SumAndMultiplyDigits;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CurrentTask = Tasks.FullHoursSinceMidnight;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            CurrentTask = Tasks.FullMinutesSinceLastHour;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            CurrentTask = Tasks.RectanglePerimeter;
        }



        private void MakeCalculations(object sender, EventArgs e)
        {
            TextBoxValidatorService.RunChecks(Checks, Fields, result);
            
            switch (CurrentTask)
            {
                case Tasks.NotSet:
                    result.Text = "Задача не была выбрана";
                    break;
                case Tasks.SumAndMultiply:
                    var a = int.Parse(textBox1.Text);
                    var b = int.Parse(textBox2.Text);
                    var c = int.Parse(textBox3.Text);
                    result.Text = $"Сумма {a + b + c}, произведение {a * b * c}";
                    break;
                case Tasks.PerimeterAndSquare:
                    var square = uint.Parse(textBox1.Text);
                    result.Text = $"Периметр квадрата {square * 4}, площадь {square * square}";
                    break;
                case Tasks.CubeVAndSquare:
                    var cube = uint.Parse(textBox1.Text);
                    result.Text = $"Объем куба {cube * cube * cube}, площадь {cube * cube * 6}";
                    break;
                case Tasks.Average:
                    var a1 = int.Parse(textBox1.Text);
                    var a2 = int.Parse(textBox2.Text);
                    var a3 = int.Parse(textBox3.Text);
                    result.Text = $"Среднее арифметическое {(a1 + a2 + a3) / 3}";
                    break;
                case Tasks.Candies:
                    var Xkg = uint.Parse(textBox1.Text);
                    var Arub = uint.Parse(textBox2.Text);
                    var YKg = uint.Parse(textBox3.Text);
                    result.Text = $"{YKg}кг конфет будут стоить {YKg * (Arub / Xkg):F2}р.";
                    break;
                case Tasks.Kilobytes:
                    var bytes = uint.Parse(textBox1.Text);
                    result.Text = $"{bytes} байтов это {(decimal) bytes / 1024:F2} КБайт";
                    break;
                case Tasks.SumAndMultiplyDigits:
                    var num = uint.Parse(textBox1.Text);
                    var digit1 = Math.DivRem(num, 10, out var digit2);
                    result.Text = $"Сумма цифр {digit1 + digit2}, произведение {digit1 * digit2}";
                    break;
                case Tasks.FullHoursSinceMidnight:
                    var seconds1 = uint.Parse(textBox1.Text);
                    var hours = TimeSpan.FromSeconds(seconds1).Hours;
                    result.Text = $"С начала суток прошло {hours} полных часов";
                    break;
                case Tasks.FullMinutesSinceLastHour:
                    var seconds2 = uint.Parse(textBox1.Text);
                    var minutes = TimeSpan.FromSeconds(seconds2).Minutes;
                    result.Text = $"С последнего целого часа прошло {minutes} минут";
                    break;
                case Tasks.RectanglePerimeter:
                    var longSide = TextBoxService.GetDecimal(textBox1);
                    var shortSide = TextBoxService.GetDecimal(textBox2);
                    result.Text = $"Периметр прямоугольника {longSide * 2 + shortSide * 2}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}