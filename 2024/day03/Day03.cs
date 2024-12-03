using System.Text.RegularExpressions;

namespace AdventOfCode.day03
{
    internal class Day03 : DailyTask
    {
        public override string PartOne()
        {
            var sum = 0;
            var mulPattern = @"mul\(\d+,\d+\)";
            var numPattern = @"\d+";
            var matches = Regex.Matches(Input, mulPattern, RegexOptions.None, TimeSpan.FromSeconds(1));

            foreach (Match match in matches)
            {
                var numbers = Regex.Matches(match.Value, numPattern, RegexOptions.None, TimeSpan.FromSeconds(1));
                sum += int.Parse(numbers.First().Value) * int.Parse(numbers.Last().Value);
            }

            return sum.ToString();
        }

        public override string PartTwo()
        {
            var sum = 0;
            var mulPattern = @"(mul\(\d+,\d+\))|(do\(\))|(don't\(\))";
            var numPattern = @"\d+";
            var matches = Regex.Matches(Input, mulPattern, RegexOptions.None, TimeSpan.FromSeconds(1));

            var dos = new List<string>();
            var doInstruction = true;
            foreach (Match match in matches)
            {
                if (match.Value == "do()")
                    doInstruction = true;
                else if (match.Value == "don't()")
                    doInstruction = false;
                else if (doInstruction)
                    dos.Add(match.Value);
            }

            foreach (string match in dos)
            {
                var numbers = Regex.Matches(match, numPattern, RegexOptions.None, TimeSpan.FromSeconds(1));
                sum += int.Parse(numbers.First().Value) * int.Parse(numbers.Last().Value);
            }

            return sum.ToString();
        }
    }
}
