namespace AdventOfCode.Day03
{
    class Day03 : DailyTask
    {
        readonly Dictionary<char, int> prioList = [];
        public override string PartOne()
        {
            FillPrioList();
            var sumPrioItems = 0;
            var rucksacks = Input.Split("\n");

            foreach (var rucksack in rucksacks)
            {
                var items = new List<char>(rucksack.ToCharArray());
                var midpoint = items.Count / 2;

                var firstCompartment = items.GetRange(0, midpoint);
                var secondCompartment = items.GetRange(midpoint, items.Count - midpoint);

                var equal = firstCompartment.Intersect(secondCompartment);
                prioList.TryGetValue(equal.First(), out int prio);
                sumPrioItems += prio;
            }

            return sumPrioItems.ToString();
        }

        public override string PartTwo()
        {
            FillPrioList();
            var sumPrioItems = 0;
            var rucksacks = new List<string>(Input.Split("\n"));

            for (int i = 0; i < rucksacks.Count; i += 3)
            {
                var group = rucksacks.GetRange(i, Math.Min(3, rucksacks.Count - i));

                var firstRucksack = new List<char>(group[0].ToCharArray());
                var secondRucksack = new List<char>(group[1].ToCharArray());
                var thirdRucksack = new List<char>(group[2].ToCharArray());

                var firstIntersect = firstRucksack.Intersect(secondRucksack);
                var finalIntersect = firstIntersect.Intersect(thirdRucksack);

                prioList.TryGetValue(finalIntersect.First(), out int prio);
                sumPrioItems += prio;
            }

            return sumPrioItems.ToString();
        }

        private void FillPrioList()
        {
            // Populate dictionary for lowercase letters a-z with priorities 1 to 26
            for (char c = 'a'; c <= 'z'; c++)
            {
                prioList[c] = c - 'a' + 1;
            }

            // Populate dictionary for uppercase letters A-Z with priorities 27 to 52
            for (char c = 'A'; c <= 'Z'; c++)
            {
                prioList[c] = c - 'A' + 27;
            }
        }
    }
}