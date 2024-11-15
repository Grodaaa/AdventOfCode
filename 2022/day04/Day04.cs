namespace AdventOfCode.Day04
{
    class Day04 : DailyTask
    {
        public override string PartOne()
        {
            var pairs = Input.Split("\n");

            var fullyContainSections = 0;
            foreach (var pair in pairs)
            {
                var sectionRanges = pair.Split(',');

                var firstElf = GetFullSectionList(sectionRanges[0]);
                var secondElf = GetFullSectionList(sectionRanges[1]);

                if (firstElf.Count < secondElf.Count || firstElf.Count == secondElf.Count)
                {
                    var equals = firstElf.Intersect(secondElf);
                    if (equals.Count() == firstElf.Count)
                        fullyContainSections++;
                }
                else
                {
                    var equals = secondElf.Intersect(firstElf);
                    if (equals.Count() == secondElf.Count)
                        fullyContainSections++;
                }
            }

            return fullyContainSections.ToString();
        }

        public override string PartTwo()
        {
            var pairs = Input.Split("\n");

            var overlapSections = 0;
            foreach (var pair in pairs)
            {
                var sectionRanges = pair.Split(',');

                var firstElf = GetFullSectionList(sectionRanges[0]);
                var secondElf = GetFullSectionList(sectionRanges[1]);

                var intersects = firstElf.Intersect(secondElf);
                if (intersects.Any())
                    overlapSections++;
            }

            return overlapSections.ToString();
        }

        private List<int> GetFullSectionList(string sectionRange)
        {
            var sections = new List<int>();
            var sectionIndexes = sectionRange.Split('-');

            for (int i = int.Parse(sectionIndexes[0]); i <= int.Parse(sectionIndexes[1]); i++)
            {
                sections.Add(i);
            }

            return sections;
        }
    }
}