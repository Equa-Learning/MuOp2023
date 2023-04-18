using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using MuOp2023.Infrastructure;

namespace MuOp2023.Practice_03_04
{
    public partial class Practice0304 : Form
    {
        public Practice0304()
        {
            InitializeComponent();
            comboBox1.DataSource = GetTaskDescriptions();
        }

        private enum Tasks
        {
            [Description("Задача не задана")] NotSet,
            [Description("Вычислить выражение")] ComputeEquation,
            [Description("Загрузить картинку")] LoadPicture,
            [Description("Четырёхзначное из двузначных")] FourDigitsFromTwoDigits,
            [Description("Кирпич и прямоугольное отверстие")] BrickAndQuad,
            [Description("Три путёвки")] ThreeTours,
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
        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedName = ((string) comboBox1.SelectedItem).ToEnumOrDefault<Tasks>();
            var labelsForTexBoxes = Array.Empty<string>();
            var checksForInputs = new List<TextBoxCheckDelegate>();
            computeButton.Text = "Вычислить";
            cleanButton.Visible = false;
            resultLabel.Text = "";

            switch (selectedName)
            {
                case Tasks.NotSet:
                    taskDefinition.Text =
                        "Задача не выбрана.\n" +
                        "Выберите задачу в выпадающем поле вверху";
                    _action = () => resultLabel.Text = "Задача не выбрана, нечего расчитывать";
                    break;
                case Tasks.ComputeEquation:
                    labelsForTexBoxes = Task_ComputeEquation(checksForInputs);
                    break;
                case Tasks.LoadPicture:
                    Task_LoadPicture();
                    break;
                case Tasks.FourDigitsFromTwoDigits:
                    labelsForTexBoxes = Task_FourDigitsFromTwoDigits(checksForInputs);
                    break;
                case Tasks.BrickAndQuad:
                    labelsForTexBoxes = Task_BrickAndQuad(labelsForTexBoxes, checksForInputs);
                    break;
                case Tasks.ThreeTours:
                    labelsForTexBoxes = Task_ThreeTours(labelsForTexBoxes, checksForInputs);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            SetupFieldsForTask(labelsForTexBoxes, checksForInputs);
        }

        private string[] Task_ThreeTours(string[] labelsForTexBoxes, List<TextBoxCheckDelegate> checksForInputs)
        {
            taskDefinition.Text =
                "В турфирме нам предложены 3 путевки (знаем стоимость N1, N2 и N3 и длительность Dl, D2 и D3 дней). " +
                "Какую из трех путевок можно выбрать, если в распоряжении X денег и У дней?";
            labelsForTexBoxes = new[] { "Сколько есть денег X", "Сколько есть дней Y" };
            checksForInputs.AddRange(new TextBoxCheckDelegate[]
            {
                TextBoxValidators.IsPositiveDecimal, TextBoxValidators.IsPositiveNumber
            });
            computeButton.Text = "Продолжить";
            _action = ActionForThreeToursStep1;

            void ActionForThreeToursStep1()
            {
                var x = TextBoxService.GetDecimal(textBox1);
                var y = uint.Parse(textBox2.Text);

                labelsForTexBoxes = new[] { "Стоимость N1", "Стоимость N2", "Стоимость N3" };
                checksForInputs.Clear();
                checksForInputs.AddRange(new TextBoxCheckDelegate[]
                {
                    TextBoxValidators.IsPositiveDecimal, TextBoxValidators.IsPositiveDecimal,
                    TextBoxValidators.IsPositiveDecimal
                });
                resultLabel.Text = $"Теперь введите Стоимость путёвок";
                //computeButton.Text = "Вычислить";

                _action = ActionForThreeToursStep2;
                SetupFieldsForTask(labelsForTexBoxes, checksForInputs);

                void ActionForThreeToursStep2()
                {
                    var n1 = TextBoxService.GetDecimal(textBox1);
                    var n2 = TextBoxService.GetDecimal(textBox2);
                    var n3 = TextBoxService.GetDecimal(textBox3);

                    labelsForTexBoxes = new[] { "Длительность D1", "Длительность D2", "Длительность D3" };
                    checksForInputs.Clear();
                    checksForInputs.AddRange(new TextBoxCheckDelegate[]
                    {
                        TextBoxValidators.IsPositiveNumber, TextBoxValidators.IsPositiveNumber,
                        TextBoxValidators.IsPositiveNumber
                    });
                    resultLabel.Text = $"Теперь введите Длительность путёвок";
                    computeButton.Text = "Вычислить";

                    _action = ActionForThreeToursStep3;
                    SetupFieldsForTask(labelsForTexBoxes, checksForInputs);

                    void ActionForThreeToursStep3()
                    {
                        var d1 = uint.Parse(textBox1.Text);
                        var d2 = uint.Parse(textBox2.Text);
                        var d3 = uint.Parse(textBox3.Text);

                        var tours = new List<( decimal Price, uint Days)>() { (n1, d1), (n2, d2), (n3, d3) };
                        var availableTours = tours
                            .Where(tour => tour.Price <= x && tour.Days <= y).ToList();
                        resultLabel.Text = $"У нас есть {x} денег и {y} дней\n";
                        if (availableTours.Count > 0)
                        {
                            resultLabel.Text += $"Можем взять туры: " + string.Join(", ",
                                availableTours
                                    .Select(tour =>
                                        $"{tour.Days} дней за {tour.Price} денег"));
                        }
                        else
                        {
                            resultLabel.Text += $"Но мы ничего не можем взять";
                        }
                    }
                }
            }

            return labelsForTexBoxes;
        }

        private string[] Task_BrickAndQuad(string[] labelsForTexBoxes, List<TextBoxCheckDelegate> checksForInputs)
        {
            taskDefinition.Text =
                "Вариант 5.	Имеется прямоугольное отверстие со сторонами а и b и кирпич с ребрами х, у, z. " +
                "Требуется определить, пройдет ли кирпич в отверстие?";
            labelsForTexBoxes = new[] { "Сторона a", "Сторона b" };
            checksForInputs.AddRange(new TextBoxCheckDelegate[]
            {
                TextBoxValidators.IsDecimal, TextBoxValidators.IsDecimal
            });
            computeButton.Text = "Продолжить";
            _action = () =>
            {
                var a = TextBoxService.GetDecimal(textBox1);
                var b = TextBoxService.GetDecimal(textBox2);
                var aIsBigger = a > b;

                labelsForTexBoxes = new[] { "ребро x", "ребро y", "ребро z" };
                checksForInputs.Clear();
                checksForInputs.AddRange(new TextBoxCheckDelegate[]
                {
                    TextBoxValidators.IsPositiveDecimal, TextBoxValidators.IsPositiveDecimal,
                    TextBoxValidators.IsPositiveDecimal
                });
                resultLabel.Text = $"Теперь введите размеры кирпича";
                computeButton.Text = "Вычислить";

                _action = ActionForBrickStep2;
                SetupFieldsForTask(labelsForTexBoxes, checksForInputs);

                void ActionForBrickStep2()
                {
                    var xyz = new Dictionary<string, decimal>()
                    {
                        { "x", TextBoxService.GetDecimal(textBox1) },
                        { "y", TextBoxService.GetDecimal(textBox2) },
                        { "z", TextBoxService.GetDecimal(textBox3) },
                    };
                    // Если min(a,b) >= x или y или z
                    // и max(a,b) >= одного из оставшихся
                    var firstPair = xyz.Where(x => x.Value <= Math.Min(a, b))
                        .OrderBy(x => x.Value)
                        .Take(1)
                        .SingleOrDefault();
                    if (firstPair.Key is null)
                    {
                        resultLabel.Text = $"НЕТ, не нашлось ребра кирпича " +
                                           $"меньше стороны {(aIsBigger ? "b" : "a")}={(aIsBigger ? b : a)}";
                    }
                    else
                    {
                        resultLabel.Text = $"Ребро {firstPair.Key}={firstPair.Value} " +
                                           $"меньше либо равно стороне {(aIsBigger ? "b" : "a")}={(aIsBigger ? b : a)}";
                        var secondPair = xyz.Where(x => x.Key != firstPair.Key)
                            .Where(x => x.Value <= Math.Max(a, b))
                            .OrderBy(x => x.Value)
                            .Take(1)
                            .SingleOrDefault();
                        if (secondPair.Key is null)
                        {
                            resultLabel.Text += $"\nНо нет ребра, " +
                                                $"больше чем {(aIsBigger ? "a" : "b")}={(aIsBigger ? a : b)}";
                            resultLabel.Text = "НЕТ, " + resultLabel.Text;
                        }
                        else
                        {
                            resultLabel.Text += $"\nИ ребро {secondPair.Key}={secondPair.Value} " +
                                                $"меньше либо равно стороне {(aIsBigger ? "a" : "b")}={(aIsBigger ? a : b)}";
                            resultLabel.Text = "ДА, " + resultLabel.Text;
                        }
                    }
                }
            };
            return labelsForTexBoxes;
        }

        private string[] Task_FourDigitsFromTwoDigits(List<TextBoxCheckDelegate> checksForInputs)
        {
            string[] labelsForTexBoxes;
            taskDefinition.Text =
                "Дано четырехзначное число. Верно ли, что в записи этого числа использованы два одинаковых двузначных числа?";
            labelsForTexBoxes = new[] { "Введите четырёхзначное число" };
            checksForInputs.AddRange(new TextBoxCheckDelegate[]
            {
                (input, log) =>
                    int.TryParse(input.Text, NumberStyles.Number, TextBoxValidatorService.RuCultureInfo,
                        out var result)
                    && Convert.ToInt32(Math.Ceiling(Math.Log10(result))) == 4
                        ? true
                        : (log.Text += $"{input.Name} не 4-значное число\n") is null
            });
            _action = () =>
            {
                var fourDigitsNumber = Math.Abs(int.Parse(textBox1.Text));
                var div100 = Math.DivRem(fourDigitsNumber, 100, out var rem100);
                if (div100 == rem100)
                {
                    resultLabel.Text = $"Да, число состоит из двух записей {div100}.";
                }
                else
                {
                    resultLabel.Text = $"Нет, число не состоит из двух одинаковых двузначных.";
                }
            };
            return labelsForTexBoxes;
        }

        private void Task_LoadPicture()
        {
            taskDefinition.Text = "Загрузить картинку";
            computeButton.Text = "Открыть";
            _action = () =>
            {
                pictureBox1.Visible = true;
                openFileDialog1.Filter = "Изображения (*.bmp;*.gif;*.jpg;*.jpeg;*.tif)|";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                    resultLabel.Text = "Картинка загружена";
                }
                else
                {
                    resultLabel.Text = "Файл не выбран";
                }
            };
        }

        private string[] Task_ComputeEquation(List<TextBoxCheckDelegate> checksForInputs)
        {
            string[] labelsForTexBoxes;
            taskDefinition.Text =
                "Составить программу вычисления выражения.\n" +
                "y= { x^2+1 | x<0; 2x+1 | 0 <= x <= 1; 4x - 1 | x > 1 }";
            labelsForTexBoxes = new[] { "Введите X" };
            checksForInputs.AddRange(new TextBoxCheckDelegate[] { TextBoxValidators.IsDecimal });
            _action = () =>
            {
                var x = TextBoxService.GetDecimal(textBox1);
                var y =
                        x < 0
                            ? x * x + 1
                            : ((x > 1)
                                ? x * 4 - 1
                                : x * 2 + 1)
                    ;
                resultLabel.Text = $"Y = {y}";
            };
            return labelsForTexBoxes;
        }


    }
}