using System.Globalization;
using System.Windows.Forms;

namespace MuOp2023.Infrastructure
{
    internal static class TextBoxValidators
    {
        public static bool AlwaysOk(TextBox input, Label log) => true;

        internal static bool IsPositiveDecimal(TextBox input, Label log) =>
            decimal.TryParse(input.Text.Replace('.', ','), NumberStyles.Number, TextBoxValidatorService.RuCultureInfo,
                out var result) && result >= 0
                ? true
                : (log.Text += $"{input.Name} не положительное число\n") is null;

        internal static bool IsDecimal(TextBox input, Label log) =>
            decimal.TryParse(input.Text.Replace('.', ','), NumberStyles.Number, TextBoxValidatorService.RuCultureInfo,
                out var result)
                ? true
                : (log.Text += $"{input.Name} не число\n") is null;

        internal static bool IsNumber(TextBox input, Label log) =>
            int.TryParse(input.Text, NumberStyles.Number, TextBoxValidatorService.RuCultureInfo, out var result)
                ? true
                : (log.Text += $"{input.Name} не число\n") is null;

        internal static bool IsPositiveNumber(TextBox input, Label log) =>
            uint.TryParse(input.Text, NumberStyles.Number, TextBoxValidatorService.RuCultureInfo, out var result)
                ? true
                : (log.Text += $"{input.Name} не положительное число\n") is null;


    }
}