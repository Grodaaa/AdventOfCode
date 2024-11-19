namespace AdventOfCode.Day11
{
    class Monkey
    {
        public List<long> Items { get; set; } = [];
        public Test Test { get; set; } = new Test();
        public Operation Operation { get; set; } = new Operation();
        public long NumberOfInspections { get; set; }
    }

    class Test
    {
        public int Divider { get; set; }
        public int IsFalse { get; set; }
        public int IsTrue { get; set; }
    }

    class Operation
    {
        public bool IsAddition { get; set; }
        public long Number { get; set; }
        public bool NumberIsSelf { get; set; }
    }
}