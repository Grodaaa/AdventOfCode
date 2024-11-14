namespace AdventOfCode.Day00
{
    class Day00 : DailyTask
    {
        public override string PartOne()
        {
            var result = ParsedInput().Aggregate("", (acc, item) => acc + item);

            return result.ToString();
        }

        public override string PartTwo()
        {
            var result = ParsedInput().Aggregate(1, (acc, item) => acc + item);

            return result.ToString();
        }

        private List<int> ParsedInput()
        {
            return Input
                        .Split('\n')
                        .ToList()
                        .ConvertAll(int.Parse);
        }
    }
}