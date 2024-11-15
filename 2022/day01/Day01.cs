namespace AdventOfCode.Day01
{
    class Day01 : DailyTask
    {
        public override string PartOne()
        {
            return GetElfCalories().OrderDescending().FirstOrDefault().ToString();
        }

        public override string PartTwo()
        {
            var elfCalories = GetElfCalories();
            return elfCalories.OrderDescending().Take(3).Sum().ToString();
        }

        private List<int> GetElfCalories() {
            var splittedInput = Input.Split("\n");
            List<int> elfs = [];
            var sumCalories = 0;

            foreach (var stringCalories in splittedInput)
            {
                if (int.TryParse(stringCalories, out int calories))
                {
                    sumCalories += calories;
                }
                else
                {
                    elfs.Add(sumCalories);
                    sumCalories = 0;
                }
            }
            elfs.Add(sumCalories);

            return elfs;
        }
    }
 }