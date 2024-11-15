using System.Diagnostics.Tracing;

namespace AdventOfCode.Day02
{
    class Day02 : DailyTask
    {
        /*
            A & X = Rock = 1
            B & Y = Paper = 2
            C & Z = Scissor = 3
        
            Win = 6
            Draw = 3
            Loss = 0
        */
        public override string PartOne()
        {
            var splittedInput = Input.Split("\n");

            var roundPoints = new List<int>();
            foreach (var round in splittedInput)
            {
                var x = round.Split(" ");
                if (x.Length != 2)
                    throw new ArgumentException($"Round input invalid: {x}");

                roundPoints.Add(GetPoints(x[0], x[1]));
            }

            return roundPoints.Sum().ToString();
        }

        /*
            A = Rock = 1
            B = Paper = 2
            C = Scissor = 3
        
            X = need to loose
            Y = need to draw
            Z = need to win

            Win = 6
            Draw = 3
            Loss = 0
        */
        public override string PartTwo()
        {
            var splittedInput = Input.Split("\n");
            var roundPoints = new List<int>();

            foreach (var round in splittedInput)
            {
                var x = round.Split(" ");
                if (x.Length != 2)
                    throw new ArgumentException($"Round input invalid: {x}");

                var shape = GetShape(x[0], x[1]);
                roundPoints.Add(GetPoints(x[0], shape));
            }

            return roundPoints.Sum().ToString();
        }

        private static int GetPoints(string elf, string me)
        {
            var points = 0;

            switch (me)
            {
                case "X":
                    points += 1;
                    break;
                case "Y":
                    points += 2;
                    break;
                case "Z":
                    points += 3;
                    break;
                default:
                    break;
            }

            if (RoundWon(elf, me))
                points += 6;
            else if (RoundDraw(elf, me))
                points += 3;

            return points;
        }

        private static bool RoundWon(string elf, string me)
        {
            if (me == "X" && elf == "C" ||
               me == "Y" && elf == "A" ||
               me == "Z" && elf == "B")
                return true;

            return false;
        }

        private static bool RoundDraw(string elf, string me)
        {
            if (me == "X" && elf == "A" ||
               me == "Y" && elf == "B" ||
               me == "Z" && elf == "C")
                return true;

            return false;
        }

        private static string GetShape(string elf, string outcome)
        {
            return outcome switch
            {
                "X" => elf switch // Needs to loose
                {
                    "A" => "Z", //Rock wins over scissor
                    "B" => "X", //Paper wins over Rock
                    "C" => "Y", //Scissor wins over paper
                    _ => throw new ArgumentException($"{outcome} is not a valid outcome"),
                },
                "Y" => elf switch // Needs to draw
                {
                    "A" => "X",
                    "B" => "Y",
                    "C" => "Z",
                    _ => throw new ArgumentException($"{outcome} is not a valid outcome"),
                },
                "Z" => elf switch // Needs to win
                {
                    "A" => "Y", //Rock looses to paper
                    "B" => "Z", //Paper looses to scissor
                    "C" => "X", //Scissor looses to rock
                    _ => throw new ArgumentException($"{outcome} is not a valid outcome"),
                },
                _ => throw new ArgumentException($"{outcome} is not a valid outcome"),
            };
        }
    }
}