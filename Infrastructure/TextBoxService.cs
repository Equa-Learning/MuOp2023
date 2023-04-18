using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace MuOp2023.Infrastructure
{
    internal static class TextBoxService
    {
        internal static void InitFields(IReadOnlyList<TextBox> inputs, IReadOnlyList<Label> labels, string[] strings)
        {
            if (inputs.Count != labels.Count) throw new IndexOutOfRangeException("Не хватает меток или полей");
            for (var i = 0; i < labels.Count; i++)
            {
                inputs[i].Visible = i < strings.Length && (labels[i].Text = strings[i]) != null 
                                    || strings.Length == 0 && (labels[i].Text = "") == null;
            }
        }
        
        internal static void CleanFields(IEnumerable<TextBox> fields)
        {
            foreach (var field in fields)
                field.Text = "";
        }        
        internal static decimal GetDecimal(TextBox textBox) =>
            decimal.Parse(textBox.Text.Replace('.', ','), 
                NumberStyles.Number, TextBoxValidatorService.RuCultureInfo);           
    }
}