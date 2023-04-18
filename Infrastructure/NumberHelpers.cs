using System;
using System.Text;

namespace MuOp2023.Infrastructure
{
    static internal class NumberHelpers
    {
        public static string NumberToText(int number)
        {
            if (number == 0)
                return "ноль";

            var absNumber = Math.Abs(number);

            if (absNumber > 1000)
                return "слишком большое число";

            var sb = new StringBuilder();

            if (number < 0)
                sb.Append("минус ");

            if (absNumber == 1000)
            {
                sb.Append("одна тысяча");
                return sb.ToString();
            }

            sb.Append((absNumber / 100) == 1 ? "сто " :
                (absNumber / 100) == 2 ? "двести " :
                (absNumber / 100) == 3 ? "триста " :
                (absNumber / 100) == 4 ? "четыреста " :
                (absNumber / 100) == 5 ? "пятьсот " :
                (absNumber / 100) == 6 ? "шестьсот " :
                (absNumber / 100) == 7 ? "семьсот " :
                (absNumber / 100) == 8 ? "восемьсот " :
                (absNumber / 100) == 9 ? "девятьсот " : "");

            sb.Append((absNumber / 10 % 10) == 2 ? "двадцать " :
                (absNumber / 10 % 10) == 3 ? "тридцать " :
                (absNumber / 10 % 10) == 4 ? "сорок " :
                (absNumber / 10 % 10) == 5 ? "пятьдесят " :
                (absNumber / 10 % 10) == 6 ? "шестьдесят " :
                (absNumber / 10 % 10) == 7 ? "семьдесят " :
                (absNumber / 10 % 10) == 8 ? "восемьдесят " :
                (absNumber / 10 % 10) == 9 ? "девяносто " : "");

            sb.Append((absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 1 ? "один" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 2 ? "два" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 3 ? "три" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 4 ? "четыре" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 5 ? "пять" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 6 ? "шесть" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 7 ? "семь" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 8 ? "восемь" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 9 ? "девять" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 10 ? "десять" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 11 ? "одинадцать" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 12 ? "двенадцать" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 13 ? "тринадцать" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 14 ? "четырнадцать" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 15 ? "пятнадцать" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 16 ? "шестнадцать" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 17 ? "семьнадцать" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 18 ? "восемнадцать" :
                (absNumber >= 20 ? absNumber % 10 : absNumber % 20) == 19 ? "девятнадцать" : "");
            return sb.ToString().Trim();
        }

        public static string YearsPadezh(int num)
        {
            switch (num)
            {
                case 0:
                    return "лет";
                case 1:
                    return "год";
                case 2:
                case 3:
                case 4:
                    return "года";
                default:
                    if (num <= 20)
                        return "лет";
                    var m = num % 10;
                    return YearsPadezh(m);
            }
        }
    }
}