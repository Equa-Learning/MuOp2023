using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using MuOp2023.Practice_01_02;

namespace MuOp2023.Infrastructure
{
    internal delegate bool TextBoxCheckDelegate(TextBox input, Label log);

    internal static class TextBoxValidatorService
    {
        internal static CultureInfo RuCultureInfo => CultureInfo.GetCultureInfo("ru-RU");

        internal static void SetupChecks(TextBoxCheckDelegate[] fieldChecks, List<TextBoxCheckDelegate> checker)
        {
            if (checker.Count > fieldChecks.Length)
                throw new IndexOutOfRangeException();

            for (var i = fieldChecks.Length-1; i > checker.Count - 1; i--)
            {
                fieldChecks[i] = TextBoxValidators.AlwaysOk;
            }

            for (var i = 0; i < checker.Count; i++)
            {
                Console.WriteLine($"checker.Count={checker.Count} setting checker {i}");
                fieldChecks[i] = checker[i];
            }
        }

        internal static bool RunChecks(TextBoxCheckDelegate[] checks, IReadOnlyList<TextBox> fields, Label resultControl)
        {
            resultControl.Text = "";
            var isOk = true;
            for (var i = 0; i < fields.Count; i++)
                isOk = checks[i](fields[i], resultControl) && isOk;

            if (isOk) return true;

            resultControl.Text = "Неверный ввод:\n" + resultControl.Text;
            return false;
        }
        
     
    }
}