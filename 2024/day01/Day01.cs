namespace AdventOfCode.day01
{
    internal class Day01 : DailyTask
    {
        public override string PartOne()
        {
            var firstList = new List<int>();
            var secondList = new List<int>();

            var diffs = new List<int>();
            var splittedInput = Input.Split(Environment.NewLine);
            foreach (var input in splittedInput)
            {
                var items = input.Split("   ");
                firstList.Add(int.Parse(items[0]));
                secondList.Add(int.Parse(items[1]));
            }
            firstList.Sort();
            secondList.Sort();

            foreach (var nw in firstList.Zip(secondList, Tuple.Create))
            {
                diffs.Add(Math.Abs(nw.Item1 - nw.Item2));
            }

            return diffs.Sum().ToString();
        }

        public override string PartTwo()
        {
            var firstList = new List<int>();
            var secondList = new List<int>();

            var nums = new List<int>();

            var diffs = new List<int>();
            var splittedInput = Input.Split(Environment.NewLine);
            foreach (var input in splittedInput)
            {
                var items = input.Split("   ");
                firstList.Add(int.Parse(items[0]));
                secondList.Add(int.Parse(items[1]));
            }

            foreach (var item in firstList)
            {
                var hits = secondList.FindAll(x => x == item);
                nums.Add(item * hits.Count);
            }

            return nums.Sum().ToString();
        }
    }
}
