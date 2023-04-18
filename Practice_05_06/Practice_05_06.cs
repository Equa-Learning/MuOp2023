using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MuOp2023.Infrastructure;

namespace MuOp2023.Practice_05_06
{
    public partial class Practice0506 : Form
    {
        public Practice0506()
        {
            InitializeComponent();
            comboBox1.DataSource = GetTaskDescriptions();
        }

        private enum Tasks
        {
            [Description("Задача не задана")] NotSet,
            [Description("Выбор картинок")] PictureSelect,
            [Description("Годы в слова")] YearsToString,
            [Description("Менять оформление")] ChangeStyle,
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
            throw new System.NotImplementedException();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedName = ((string) comboBox1.SelectedItem).ToEnumOrDefault<Tasks>();
            changeResultLabelStyle((SystemColors.Control, SystemColors.ControlText));
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
                case Tasks.PictureSelect:
                    Task_PictureSelect();
                    break;
                case Tasks.YearsToString:
                    labelsForTexBoxes = Task_YearsToString(checksForInputs);
                    break;
                case Tasks.ChangeStyle:
                    Task_ChangeStyle();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SetupFieldsForTask(labelsForTexBoxes, checksForInputs);
        }
        

        private void changeResultLabelStyle((Color Background, Color Foreground) newStyle)
        {
            resultLabel.BackColor = newStyle.Background;
            resultLabel.ForeColor = newStyle.Foreground;
        }        

        private void Task_ChangeStyle()
        {
            taskDefinition.Text =
                "Создайте проект, который позволял бы менять оформление визуальных элемен-тов на форме (использовать перечисления)";

            var styles = new Dictionary<string, (Color Background, Color Foreground)>()
            {
                { "Стандартный", (SystemColors.Control, SystemColors.ControlText) },
                { "Белый на красном", (Color.Red, Color.White) },
                { "Красный на чёрном", (Color.Black, Color.Red) }
            };
            listBox1.Visible = true;
            listBox1.DataSource = styles.Select(x => x.Key).ToList();

            listBox1.SelectedIndexChanged += (sender1, e1) =>
            {
                changeResultLabelStyle(styles[(string) listBox1.SelectedItem]);
                resultLabel.Text = $"Стиль изменён на {(string) listBox1.SelectedItem}";
            };
            _action = () => { };
        }


        private string[] Task_YearsToString(List<TextBoxCheckDelegate> checksForInputs)
        {
            string[] labelsForTexBoxes;
            taskDefinition.Text = "Дано возраст человека: целое от 1 до 99 лет. Вывести фразу «мне N лет», " +
                                  "с правильным согласованием числа со словом «год», " +
                                  "например: 20 —  «двадцать лет», 32 — «тридцать два года»";
            labelsForTexBoxes = new[] { "Сколько вам лет" };
            checksForInputs.AddRange(new TextBoxCheckDelegate[]
            {
                (TextBox input, Label log) =>
                    uint.TryParse(input.Text, NumberStyles.Number, TextBoxValidatorService.RuCultureInfo,
                        out var result)
                    && result >= 1 && result <= 99
                        ? true
                        : (log.Text += $"{input.Name} не число от 1 до 99\n") is null
            });
            _action = () =>
            {
                var years = uint.Parse(textBox1.Text);
                resultLabel.Text = $"Вам {NumberHelpers.NumberToText((int) years)} " +
                                   $"{NumberHelpers.YearsPadezh((int) years)}";
            };
            return labelsForTexBoxes;
        }

        private void Task_PictureSelect()
        {
            taskDefinition.Text = "Отобразить картинку в зависимости от времени года";
            pictureBox1.Visible = true;
            var pathbase = Environment.CurrentDirectory;
            pathbase = pathbase.Substring(0, pathbase.LastIndexOf(@"\", pathbase.LastIndexOf(@"\") - 1)) + @"\";
            var paths = new Dictionary<string, string>()
            {
                { "не выбрана", @"Practice_05_06\pictures\winter.jpg" },
                { "Зима", @"Practice_05_06\pictures\winter.jpg" },
                { "Весна", @"Practice_05_06\pictures\spring.jpg" },
                { "Лето", @"Practice_05_06\pictures\summer.jpg" },
                { "Осень", @"Practice_05_06\pictures\autumn.jpg" },
            };
            listBox1.Visible = true;
            listBox1.DataSource = paths.Select(x => x.Key).ToArray();

            listBox1.SelectedIndexChanged += (sender1, e1) =>
                pictureBox1.Image = listBox1.SelectedItem.ToString() == "не выбрана"
                    ? null
                    : Image.FromFile(pathbase + paths[listBox1.SelectedItem.ToString()]);
        }
    }
}